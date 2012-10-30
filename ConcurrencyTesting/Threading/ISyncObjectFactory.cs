using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Threading
{
  public interface ISyncObjectFactory
  {
    IReaderWriterLock CreateReaderWriterLock(object resource);
  }
}
