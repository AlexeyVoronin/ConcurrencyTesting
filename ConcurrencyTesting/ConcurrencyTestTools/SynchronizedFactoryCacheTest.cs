
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using Asteros.Abc.Common.Factories;
using NMock;
using NUnit.Framework;

namespace ChessTest
{
	[TestFixture]
	public class SynchronizedFactoryCacheTest
	{
		[Test]
		public void SimpleTest()
		{
			MockFactory mockFactory = new MockFactory();
			
			var obj1 = new object();
			var obj2 = new object();
			var key1 = 1;
			var key2 = 2;
			
			var factoryMock = mockFactory.CreateMock<IFactory<int, object>>(); 
			factoryMock.Stub.Out.MethodWith(o => o.Create(key1)).WillReturn(obj1);
			factoryMock.Stub.Out.MethodWith(o => o.Create(key2)).WillReturn(obj2);
			
			var factory = factoryMock.MockObject;
			var factoryCache = new SynchronizedFactoryCache<int, object>(factory);
			
			Assert.IsTrue(ReferenceEquals(obj1, factoryCache.Create(key1)));
			Assert.IsTrue(ReferenceEquals(obj2, factoryCache.Create(key2)));
			Assert.IsTrue(ReferenceEquals(obj2, factoryCache.Create(key2)));
			
			mockFactory.VerifyAllExpectationsHaveBeenMet();
		}
		
		[Test]
		public void RegisterMethodTest()
		{
			MockFactory mockFactory = new MockFactory();
			
			var obj1 = new object();
			var obj2 = new object();
			var key1 = 1;
			var key2 = 2;
			
			var factoryMock = mockFactory.CreateMock<IFactory<int, object>>(); 
			factoryMock.Stub.Out.MethodWith(o => o.Create(key1)).WillReturn(obj1);
			factoryMock.Stub.Out.MethodWith(o => o.Create(key2)).WillReturn(obj2);
			
			var factory = factoryMock.MockObject;
			var factoryCache = new SynchronizedFactoryCache<int, object>(factory);
			
			factoryCache.Create(key1);
			factoryCache.Create(key2);
			factoryCache.Create(key2);
			
			var accessInfo = factoryCache.AccessManager.GetMethodAccessInfo();
			Assert.AreEqual(5, accessInfo.Count());
			
			ValidateAccessRules(accessInfo);

			mockFactory.VerifyAllExpectationsHaveBeenMet();
		}
		
		private static void ValidateAccessRules(IEnumerable<MethodAccessInfo> accessInfo)
		{
			var t = accessInfo.GroupBy(
				o => o,
				o => accessInfo.Where(
					i => i != o && 
					i.Start.Ticks >= o.Start.Ticks && 
					i.Start.Ticks <= o.End.Ticks));
						
			foreach (var element in accessInfo) {
				var elementsWithSameTime = accessInfo.Where(
					i => i != element && 
					i.Start.Ticks >= element.Start.Ticks && 
					i.Start.Ticks <= element.End.Ticks).ToList();
				
							
				if (elementsWithSameTime.Any())
					Assert.Fail("Detected concurent access: {0}{1}{0}{0}{2}", 
					            Environment.NewLine,
					            element,
					            elementsWithSameTime.First());
			}			
		}

    [Test]
    public void SimpleMultiThreadTest()
    {
      MockFactory mockFactory = new MockFactory();

      var obj1 = new object();
      var obj2 = new object();
      var key1 = 1;
      var key2 = 2;

      var factoryMock = mockFactory.CreateMock<IFactory<int, object>>();
      factoryMock.Stub.Out.MethodWith(o => o.Create(key1)).WillReturn(obj1);
      factoryMock.Stub.Out.MethodWith(o => o.Create(key2)).WillReturn(obj2);

      var factory = factoryMock.MockObject;
      var factoryCache = new SynchronizedFactoryCache<int, object>(factory);

      factoryCache.Create(key1);
      factoryCache.Create(key2);
      factoryCache.Create(key2);

      var accessInfo = factoryCache.AccessManager.GetMethodAccessInfo().ToList();
      Assert.AreEqual(5, accessInfo.Count());

      ValidateAccessRules(accessInfo);

      foreach (var element in accessInfo)
      {
        factoryCache = new SynchronizedFactoryCache<int, object>(factory);
        factoryCache.AccessManager.ClearAccessInfo();
        ThreadStart action = () => factoryCache.Create(key1);
        var thread1 = new Thread((ThreadStart)action);
        var thread2 = new Thread((ThreadStart)action);

        var waitHandle = factoryCache.AccessManager.UnblockCurrentThreadsAndBlock(
          thread2.ManagedThreadId, element.Method);

        thread2.Start();
        waitHandle.WaitOne();
        thread1.Start();
        thread1.Join(1000);

        factoryCache.AccessManager.UnblockCurrentThreadsAndBlock(
          0, null);

        Assert.IsTrue(thread2.Join(1000));
        Assert.IsTrue(thread1.Join(1000));

        var i = factoryCache.AccessManager.GetMethodAccessInfo();
        ValidateAccessRules(i);
      }


      mockFactory.VerifyAllExpectationsHaveBeenMet();
    }
	}
}
