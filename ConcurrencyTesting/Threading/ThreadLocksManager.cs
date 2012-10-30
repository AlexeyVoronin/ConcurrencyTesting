using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Threading;

namespace Threading
{
  public sealed class ThreadLocksManager : IThreadLocksManager
  {
    #region IThreadLocksManager Members

    public void EnterLock(Action enterLockAction, params object[] syncObjects)
    {
    }

    public void ExitLock(Action exitLockAction, params object[] syncObjects)
    {
    }

    #endregion
  }
}
