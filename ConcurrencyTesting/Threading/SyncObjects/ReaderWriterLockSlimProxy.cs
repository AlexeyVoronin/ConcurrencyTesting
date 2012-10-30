using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Threading.SyncObjects
{
  public sealed class ReaderWriterLockSlimProxy : IReaderWriterLock
  {
    #region IReaderWriterLock Members

    public void EnterWriteLock()
    {
      _readerWriterLock.EnterWriteLock();
    }

    public void ExitWriteLock()
    {
      _readerWriterLock.ExitWriteLock();
    }

    public void EnterReadLock()
    {
      _readerWriterLock.EnterReadLock();
    }

    public void ExitReadLock()
    {
      _readerWriterLock.ExitReadLock();
    }

    #endregion

    #region IDisposable Members

    public void Dispose()
    {
      _readerWriterLock.Dispose();
    }

    #endregion

    private readonly ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();
  }
}
