using System;
using Castle.DynamicProxy;

namespace ConcurrencyTestTools
{
    public class ThreadUnsafeResource<TResource> : IThreadUnsafeResource
        where TResource : class
    {
        public ThreadUnsafeResource(TResource resource)
        {
            _resource = resource;
        }

        public TResource GetProxy()
        {
            var proxyGenerator = new ProxyGenerator();
            return proxyGenerator.CreateInterfaceProxyWithTarget(_resource, new Interceptor(Intercept));
        }

        public event EventHandler<EventArgs> InvocationIntercepted;

        private void Intercept(IInvocation invocation)
        {
            OnInvocationIntercepted(new EventArgs());
            invocation.Proceed();
        }

        private void OnInvocationIntercepted(EventArgs e)
        {
            EventHandler<EventArgs> handler = InvocationIntercepted;
            if (handler != null) handler(this, e);
        }

        private readonly TResource _resource;

        private class Interceptor : IInterceptor
        {
            public Interceptor(Action<IInvocation> intercept)
            {
                _intercept = intercept;
            }

            public void Intercept(IInvocation invocation)
            {
                _intercept(invocation);
            }

            private readonly Action<IInvocation> _intercept;
        }
    }
}
