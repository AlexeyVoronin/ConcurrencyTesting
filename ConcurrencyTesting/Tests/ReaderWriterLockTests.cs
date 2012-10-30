using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NMock;
using Threading;
using Threading.SyncObjects;

namespace Tests
{
  [TestClass]
  public class ReaderWriterLockTests
  {
    [TestMethod]
    public void ReadAccess()
    {
      var threadLocksManagerMock = _mockFactory.CreateMock<IThreadLocksManager>();

      var readerWriterLock = new ReaderWriterLock(threadLocksManagerMock.MockObject);

      readerWriterLock.EnterReadLock();
      readerWriterLock.ExitReadLock();
    }

    [TestCleanup]
    public void TestCleanup()
    {
      _mockFactory.VerifyAllExpectationsHaveBeenMet();
    }

    private readonly MockFactory _mockFactory = new MockFactory();
  }
}
