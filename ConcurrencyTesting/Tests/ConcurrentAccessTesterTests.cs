using ConcurrencyTestTools;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;

namespace Tests
{
    [TestClass]
    public class ConcurrentAccessTesterTests
    {
        [TestMethod]
        [ExpectedException(typeof(UnsafeThreadAccessException))]
        public void SimpleUnsafeAccessTest()
        {
            var threadUnsafeResource = _mockFactory.CreateMock<IThreadUnsafeResource1>();
            threadUnsafeResource.Stub.Out.Method(r => r.DoSomething());

            var tester = new ConcurrentAccessTester();
            var threadUnsafeResourceProxy = tester.RegisterThreadUnsafeResource(threadUnsafeResource.MockObject)
                .GetProxy();
            tester.RegisterAccessScenario(threadUnsafeResourceProxy.DoSomething);
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
