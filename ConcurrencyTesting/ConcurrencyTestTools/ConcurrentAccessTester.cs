using System;
using System.Collections.Generic;
using System.Threading;

namespace ConcurrencyTestTools
{
    public class ConcurrentAccessTester
    {
        public ThreadUnsafeResource<TResource> RegisterThreadUnsafeResource<TResource>(
            TResource resource)
            where TResource : class
        {
            var threadUnsafeResource = new ThreadUnsafeResource<TResource>(resource);
            _threadUnsafeResources.Add(resource, threadUnsafeResource);
            return threadUnsafeResource;
        }

        public void RegisterAccessScenario(Action scenario)
        {
            _scenarios.Add(scenario);
        }

        public void Test()
        {
            foreach (var scenario in _scenarios)
            {
                TestScenarioPair(scenario, scenario);
            }
        }

        private void TestScenarioPair(Action scenario1, Action scenario2)
        {
            var thread1 = new Thread(new ThreadStart(scenario1));
            var thread2 = new Thread(new ThreadStart(scenario2));

            thread1.Start();
            thread2.Start();
        }

        private readonly ICollection<Action> _scenarios = new List<Action>();
        private readonly Dictionary<object, object> _threadUnsafeResources 
            = new Dictionary<object, object>();
    }
}
