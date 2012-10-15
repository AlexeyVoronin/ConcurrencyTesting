using System;
using System.Threading;
using Castle.DynamicProxy;

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
        }

        private void _threadUnsafeResource_InvocationIntercepted(object sender, EventArgs e)
        {
            InterceptResourceInvocation();
        }

        private void InterceptResourceInvocation()
        {
            
        }

        private readonly Action _scenario1;
        private readonly Action _scenario2;
        private readonly IThreadUnsafeResource _threadUnsafeResource;
    }
}
