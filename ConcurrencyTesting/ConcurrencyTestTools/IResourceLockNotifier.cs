using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrencyTestTools
{
  public interface IResourceLockNotifier
  {
    event EventHandler<ResourceLockEventArgs> ResourceLocked;

    event EventHandler<ResourceLockEventArgs> ResourceUnlocked;
  }
}
