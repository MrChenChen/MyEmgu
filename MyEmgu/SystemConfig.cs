using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyEmgu
{




    public static class SystemConfig
    {

        public static SysStation st { set; get; } = SysStation.SI;

    }

    public enum SysStation
    {
        SI,
        MI,
        SIMI
    }

}
