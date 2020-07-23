using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace APIPreventivas.Domain.Enum
{
    public class AnfMgEnum
    {
        public enum ANF_MG 
        {
            [Description("31")] BH, 
            [Description("32")] JF, 
            [Description("33")] GV, 
            [Description("34")] UR, 
            [Description("35")] VG, 
            [Description("37")] DV 
        }
    }
}
