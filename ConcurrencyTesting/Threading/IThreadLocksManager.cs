using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Threading
{
  public interface IThreadLocksManager
  {
    void EnterLock(Action enterLockAction, params object[] syncObjects);

    void ExitLock(Action exitLockAction, params object[] syncObjects);
  }
}
