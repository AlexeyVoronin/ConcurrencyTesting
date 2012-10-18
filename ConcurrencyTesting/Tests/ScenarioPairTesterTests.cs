using System;
using ConcurrencyTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;

namespace Tests
{
    [TestClass]
    public class ScenarioPairTesterTests
    {
        [TestMethod]
        public void NoOneAccessedResourceTest()
        {
            var threadUnsafeResourceMock = _mockFactory.CreateMock<IThreadUnsafeResource>();
            threadUnsafeResourceMock.Expects.AtLeastOne.EventBinding(r => r.InvocationIntercepted += null);

            var tester = new ScenarioPairTester(() => { }, () => { }, threadUnsafeResourceMock.MockObject);
            tester.Test();
        }

        [TestMethod]
        public void OneThreadAccessedResourceTest()
        {
            var threadUnsafeResourceMock = _mockFactory.CreateMock<IThreadUnsafeResource>();
            var eventInvokerMock = threadUnsafeResourceMock.Expects
                .AtLeastOne.EventBinding(r => r.InvocationIntercepted += null);
            
            var tester = new ScenarioPairTester(
                () => eventInvokerMock.Invoke(new EventArgs()), 
                () => { }, 
                threadUnsafeResourceMock.MockObject);

            tester.Test();
        }

        [TestMethod]
        [ExpectedException(typeof(UnsafeThreadAccessException))]
        public void TwoThreadAccessedResourceTest()
        {
            var threadUnsafeResourceMock = _mockFactory.CreateMock<IThreadUnsafeResource>();
            var eventInvokerMock = threadUnsafeResourceMock.Expects
                .AtLeastOne.EventBinding(r => r.InvocationIntercepted += null);

            var tester = new ScenarioPairTester(
                () => eventInvokerMock.Invoke(new EventArgs()),
                () => eventInvokerMock.Invoke(new EventArgs()),
                threadUnsafeResourceMock.MockObject);

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
