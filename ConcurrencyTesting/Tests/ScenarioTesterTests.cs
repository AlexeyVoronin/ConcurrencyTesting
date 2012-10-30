using System;
using ConcurrencyTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;

namespace Tests
{
  [TestClass]
  public class ScenarioTesterTests
  {
    [TestMethod]
    public void NoOneAccessedResourceTest()
    {
      var resourceLockNotifier = _mockFactory.CreateMock<IResourceLockNotifier>();
      var threadUnsafeResourceMock = _mockFactory.CreateMock<IThreadUnsafeResource>();
      threadUnsafeResourceMock.Expects.AtLeastOne.EventBinding(r => r.InvocationIntercepted += null);

      var tester = new ScenarioTester(
        () => { }, resourceLockNotifier.MockObject, threadUnsafeResourceMock.MockObject);
      tester.Test();
    }

    [TestMethod]
    [ExpectedException(typeof(UnsafeThreadAccessException))]
    public void OneThreadAccessedResourceTest()
    {
      var resourceLockNotifier = _mockFactory.CreateMock<IResourceLockNotifier>();
      var threadUnsafeResourceMock = _mockFactory.CreateMock<IThreadUnsafeResource>();
      var eventInvokerMock = threadUnsafeResourceMock.Expects
          .AtLeastOne.EventBinding(r => r.InvocationIntercepted += null);

      var tester = new ScenarioTester(
          () => eventInvokerMock.Invoke(new EventArgs()),
          resourceLockNotifier.MockObject,
          threadUnsafeResourceMock.MockObject);

      tester.Test();
    }

    [TestMethod]
    public void AccessToResourceWithSync()
    {
      var resourceLockNotifier = _mockFactory.CreateMock<IResourceLockNotifier>();
      var resourceLockedEventDispatcher = resourceLockNotifier.Expects
            .AtLeastOne.EventBinding(r => r.ResourceLocked += null);
      var resourceUnlockedEventDispatcher = resourceLockNotifier.Expects
            .AtLeastOne.EventBinding(r => r.ResourceUnlocked += null);
      
      var threadUnsafeResourceMock = _mockFactory.CreateMock<IThreadUnsafeResource>();
      var invocationInterceptedEventDispatcher = threadUnsafeResourceMock.Expects
            .AtLeastOne.EventBinding(r => r.InvocationIntercepted += null);

      Action scenario = () =>
        {
          resourceLockedEventDispatcher.Invoke(new ResourceLockEventArgs(threadUnsafeResourceMock));
          invocationInterceptedEventDispatcher.Invoke(new EventArgs());
          resourceUnlockedEventDispatcher.Invoke(new ResourceLockEventArgs(threadUnsafeResourceMock));
        };

      var tester = new ScenarioTester(
        scenario, resourceLockNotifier.MockObject, threadUnsafeResourceMock.MockObject);

      tester.Test();
    }

    [TestMethod]
    [ExpectedException(typeof(UnsafeThreadAccessException))]
    public void AccessToResourceWithSyncInvalidResource()
    {
      var resourceLockNotifier = _mockFactory.CreateMock<IResourceLockNotifier>();
      var resourceLockedEventDispatcher = resourceLockNotifier.Expects
            .AtLeastOne.EventBinding(r => r.ResourceLocked += null);
      var resourceUnlockedEventDispatcher = resourceLockNotifier.Expects
            .AtLeastOne.EventBinding(r => r.ResourceUnlocked += null);

      var threadUnsafeResourceMock = _mockFactory.CreateMock<IThreadUnsafeResource>();
      var invocationInterceptedEventDispatcher = threadUnsafeResourceMock.Expects
            .AtLeastOne.EventBinding(r => r.InvocationIntercepted += null);

      Action scenario = () =>
      {
        resourceLockedEventDispatcher.Invoke(new ResourceLockEventArgs(new object()));
        invocationInterceptedEventDispatcher.Invoke(new EventArgs());
        resourceUnlockedEventDispatcher.Invoke(new ResourceLockEventArgs(new object()));
      };

      var tester = new ScenarioTester(
        scenario, resourceLockNotifier.MockObject, threadUnsafeResourceMock.MockObject);

      tester.Test();
    }

    [TestCleanup]
    public void TestCleanup()
    {
      _mockFactory.VerifyAllExpectationsHaveBeenMet();
    }

    private readonly MockFactory _mockFactory = new MockFactory();
  }
}
