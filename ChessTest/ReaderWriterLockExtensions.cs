using System;
using System.Threading;

namespace Asteros.Abc.Common.Threading
{
    internal static class ReaderWriterLockExtensions
    {
        public static T WithWriteSynchronized<T>(this ReaderWriterLockSlim readerWriterLock, Func<T> action)
        {
            readerWriterLock.EnterWriteLock();

            try
            {
                return action();
            }
            finally
            {
                readerWriterLock.ExitWriteLock();
            }
        }

        public static T WithReadSynchronized<T>(this ReaderWriterLockSlim readerWriterLock, Func<T> action)
        {
            readerWriterLock.EnterReadLock();

            try
            {
                return action();
            }
            finally
            {
                readerWriterLock.ExitReadLock();
            }
        }

        public static void WithWriteSynchronized(this ReaderWriterLockSlim readerWriterLock, Action action)
        {
            WithWriteSynchronized(
                readerWriterLock,
                () =>
                    {
                        action();
                        return 0;
                    });
        }

        public static void WithReadSynchronized(this ReaderWriterLockSlim readerWriterLock, Action action)
        {
            WithReadSynchronized(
                readerWriterLock,
                () =>
                    {
                        action();
                        return 0;
                    });
        }
    }
}
