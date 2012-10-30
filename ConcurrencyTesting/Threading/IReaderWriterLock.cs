using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Threading
{
  public interface IReaderWriterLock : IDisposable
  {
    void EnterWriteLock();

    void ExitWriteLock();

    void EnterReadLock();

    void ExitReadLock();
  }
}
