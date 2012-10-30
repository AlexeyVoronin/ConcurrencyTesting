using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConcurrencyTestTools
{
  public sealed class ResourceLockEventArgs : EventArgs
  {
    public object Resource { get; private set; }

    public ResourceLockEventArgs(object resource)
    {
      Resource = resource;
    }
  }
}
