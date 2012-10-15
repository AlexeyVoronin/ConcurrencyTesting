using System;
using System.Reflection;
using System.Threading;

namespace ConcurrencyTestTools
{
    public sealed class MethodAccessInfo
    {
        public Thread Thread { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public MethodInfo Method { get; set; }
        public bool IsThreadSafe { get; set; }

        override public string ToString()
        {
            return string.Format("{0} [{1}] ({2:o} - {3:o})",
                                 Method,
                                 Thread.ManagedThreadId,
                                 Start,
                                 End);
        }
    }
}
