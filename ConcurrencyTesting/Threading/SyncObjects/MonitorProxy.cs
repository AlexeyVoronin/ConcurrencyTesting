using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Threading.SyncObjects
{
  public sealed class MonitorProxy : IMonitor
  {
    public MonitorProxy(object resource)
    {
      _resource = resource;

      Monitor.Enter(_resource);
    }

    #region IDisposable Members

    public void Dispose()
    {
      Monitor.Exit(_resource);
    }

    #endregion

    private readonly object _resource;
  }
}
