using System;
using System.Threading;

namespace ConcurrencyTestTools
{
  public class ScenarioPairTester : IScenarioPairTester
  {
    public ScenarioPairTester(Action scenario1, Action scenario2, IThreadUnsafeResource threadUnsafeResource)
    {
      _scenario1 = scenario1;
      _scenario2 = scenario2;
      _threadUnsafeResource = threadUnsafeResource;
    }

    public void Test()
    {
      _threadUnsafeResource.InvocationIntercepted +=
        _threadUnsafeResource_InvocationIntercepted;

      var thread1 = new Thread(new ThreadStart(_scenario1));
      var thread2 = new Thread(new ThreadStart(_scenario2));

      thread1.Start();
      thread2.Start();

      thread1.Join();
      thread2.Join();

      if (_isUnsafeInvocationDetected)
        throw new UnsafeThreadAccessException();
    }

    private void _threadUnsafeResource_InvocationIntercepted(object sender, EventArgs e)
    {
      InterceptResourceInvocation();
    }

    private void InterceptResourceInvocation()
    {
      lock (_syncObj)
      {
        if (_isUnsafeInvocationDetected)
          return;

        if (_isThreadWorking)
        {
          _isUnsafeInvocationDetected = true;
          _threadSync.Set();
          return;
        }

        _isThreadWorking = true;
        _threadSync.Reset();
      }

      _threadSync.WaitOne();
    }

    private readonly Action _scenario1;
    private readonly Action _scenario2;
    private readonly IThreadUnsafeResource _threadUnsafeResource;
    private bool _isUnsafeInvocationDetected;
    private readonly ManualResetEvent _threadSync = new ManualResetEvent(false);
    private readonly object _syncObj = new object();
    private bool _isThreadWorking;
  }
}
