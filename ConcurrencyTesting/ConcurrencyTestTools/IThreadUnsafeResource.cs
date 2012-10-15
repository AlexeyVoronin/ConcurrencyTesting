using System;

namespace ConcurrencyTestTools
{
    public interface IThreadUnsafeResource
    {
        event EventHandler<EventArgs> InvocationIntercepted;
    }
}
