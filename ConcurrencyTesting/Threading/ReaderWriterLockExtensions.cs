using System;
using System.Threading;

namespace Threading
{
  public static class ReaderWriterLockExtensions
  {
    public static T WithWriteSynchronized<T>(this IReaderWriterLock readerWriterLock, Func<T> action)
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

    public static T WithReadSynchronized<T>(this IReaderWriterLock readerWriterLock, Func<T> action)
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

    public static void WithWriteSynchronized(this IReaderWriterLock readerWriterLock, Action action)
    {
      WithWriteSynchronized(
          readerWriterLock,
          () =>
          {
            action();
            return 0;
          });
    }

    public static void WithReadSynchronized(this IReaderWriterLock readerWriterLock, Action action)
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
