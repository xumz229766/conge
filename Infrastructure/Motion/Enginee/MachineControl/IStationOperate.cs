using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMotion.Applications
{
    public interface IStationOperate
    {
        bool RunningSign { set; }
        bool SingleRunning { get;}
        bool Running { get;  }
        StationStatus Status { get;  }
    }
}
