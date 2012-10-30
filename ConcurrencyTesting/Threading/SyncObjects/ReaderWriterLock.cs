using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Threading.SyncObjects
{
  public sealed class ReaderWriterLock : IReaderWriterLock
  {
    public ReaderWriterLock(IThreadLocksManager locksManager)
    {
      _locksManager = locksManager;
    }

    #region IReaderWriterLock Members

    public void EnterWriteLock()
    {
      _locksManager.EnterLock(() => _readerWriterLock.EnterWriteLock(), _readLock, _writeLock);
    }

    public void ExitWriteLock()
    {
      _locksManager.ExitLock(() => _readerWriterLock.ExitWriteLock(), _readLock, _writeLock);
    }

    public void EnterReadLock()
    {
      _locksManager.EnterLock(() => _readerWriterLock.EnterReadLock(), _writeLock);
    }

    public void ExitReadLock()
    {
      _locksManager.ExitLock(() => _readerWriterLock.ExitReadLock(), _writeLock);
    }

    #endregion

    #region IDisposable Members

    public void Dispose()
    {
      _readerWriterLock.Dispose();
    }

    #endregion

    private readonly ReaderWriterLockSlim _readerWriterLock = new ReaderWriterLockSlim();
    private readonly IThreadLocksManager _locksManager;
    private readonly object _readLock = new object();
    private readonly object _writeLock = new object();
  }
}
