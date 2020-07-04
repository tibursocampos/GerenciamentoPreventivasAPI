using System.Collections.Generic;
using System.ComponentModel;

namespace APIPreventivas.Models
{
    public class Supervisor : Funcionario
    {
        public enum ANF_MG {[Description("31")] BH, [Description("32")] JF, [Description("33")] GV, [Description("34")] UR, [Description("35")] VG, [Description("37")] DV }
        public ANF_MG ANF { get; set; }
        public ICollection<Cronograma> Cronogramas { get; set; }

    }
}


