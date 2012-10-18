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
            var threadUnsafeResourceMock = _mockFactory.CreateMock<IThreadUnsafeResource>();
            threadUnsafeResourceMock.Expects.AtLeastOne.EventBinding(r => r.InvocationIntercepted += null);

            var tester = new ScenarioTester(() => { }, threadUnsafeResourceMock.MockObject);
            tester.Test();
        }

        [TestMethod]
        [ExpectedException(typeof(UnsafeThreadAccessException))]
        public void OneThreadAccessedResourceTest()
        {
            var threadUnsafeResourceMock = _mockFactory.CreateMock<IThreadUnsafeResource>();
            var eventInvokerMock = threadUnsafeResourceMock.Expects
                .AtLeastOne.EventBinding(r => r.InvocationIntercepted += null);
            
            var tester = new ScenarioTester(
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
