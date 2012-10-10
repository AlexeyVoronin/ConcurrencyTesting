using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace ConcurrencyTestTools
{
    public sealed class ThreadAccessManager<T>
		where T : class
	{
		public ThreadAccessManager(T wrappedObject)
		{
			_wrappedObject = wrappedObject;
		}
		
		public IEnumerable<MethodAccessInfo> GetMethodAccessInfo()
		{
			return _registeredAccess;
		}
		
		public void ClearAccessInfo()
		{
			_registeredAccess.Clear();
		}

    public ThreadAccessManager<T> RegisterThreadSafeMethod<TResult>(
      Expression<Func<T, TResult>> invokeExpression)
    {
      var threadSafeMethod = ExtractMethodInfoFromExpression(invokeExpression);

      _threadSafeMethods.Add(threadSafeMethod);

      return this;
    }

    private readonly IList<MethodInfo> _threadSafeMethods = new List<MethodInfo>();
		
		public WaitHandle UnblockCurrentThreadsAndBlock(
			int threadId, MethodInfo method)
		{
			var currentWaitHandle = _waitHandle;
			_waitHandle = new AutoResetEvent(false);
			_blockedThreadId = threadId;
			_blockedMethod = method;
			if (currentWaitHandle != null)
				currentWaitHandle.Set();
			
			_startBlockWaitHandle = new AutoResetEvent(false);
			
			return _startBlockWaitHandle;
		}
		
		public TResult Invoke<TResult>(Expression<Func<T, TResult>> invokeExpression)
		{
			if (Thread.CurrentThread.ManagedThreadId == _blockedThreadId &&
			    ExtractMethodInfoFromExpression(invokeExpression) == _blockedMethod &&
			    _waitHandle != null)
			{
				_startBlockWaitHandle.Set();
				_waitHandle.WaitOne();				
			}

      Thread.Sleep(100);
			var accessInfo = RegisterMethodCallBegin(invokeExpression);
      Thread.Sleep(100);
			var result = invokeExpression.Compile().Invoke(_wrappedObject);
			Thread.Sleep(100);
			RegisterMethodCallEnd(accessInfo);
      Thread.Sleep(100);
			return result;
		}
		
		private MethodAccessInfo RegisterMethodCallBegin<TResult>(		
			Expression<Func<T, TResult>> invokeExpression)
		{			
			var method = ExtractMethodInfoFromExpression(invokeExpression);
			var accessInfo = new MethodAccessInfo
			{
				CallExpression = invokeExpression,
				Start = DateTime.Now,
				Method = method,
				Thread = Thread.CurrentThread,
        IsThreadSafe = _threadSafeMethods.Contains(method)
			};
			lock(_registeredAccess)
			{
				_registeredAccess.Add(accessInfo);
			}
			return accessInfo;
		}
		
		private void RegisterMethodCallEnd(MethodAccessInfo accessInfo)
		{
			accessInfo.End = DateTime.Now;
		}
		
		private MethodInfo ExtractMethodInfoFromExpression<TResult>(		
			Expression<Func<T, TResult>> invokeExpression)
		{
			var methodCallExp = invokeExpression.Body as MethodCallExpression;
			if (methodCallExp == null)
				throw new ArgumentException("Invalid expression", "invokeExpression");
			
			return methodCallExp.Method;
		}
		                                                   
		private readonly T _wrappedObject;
		private readonly IList<MethodAccessInfo> _registeredAccess = 
			new List<MethodAccessInfo>();		
		private int _blockedThreadId;
		private MethodInfo _blockedMethod;
		private AutoResetEvent _waitHandle;
		private AutoResetEvent _startBlockWaitHandle;
		
	}

    public sealed class MethodAccessInfo
	{
		public Thread Thread { get; set; }
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public LambdaExpression CallExpression { get; set; }
		public MethodInfo Method { get; set; }
    public bool IsThreadSafe { get; set; }
		
		override public string ToString()
		{
			return string.Format("{0} [{1}] ({2:o} - {3:o})",
			                     CallExpression,
			                     Thread.ManagedThreadId,
			                     Start,
			                     End);
		}
	}
}
