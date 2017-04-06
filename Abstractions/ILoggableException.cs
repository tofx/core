using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOF.Core.Abstractions
{
    public interface ILoggableException
    {
        string WriteLog(ILogger logger);
    }
}
