using System.Collections.Generic;
using System.ComponentModel;
using static APIPreventivas.Domain.Enum.AnfMgEnum;

namespace APIPreventivas.Models
{
    public class Supervisor : Funcionario
    {
        public ANF_MG ANF { get; set; }
        public ICollection<Cronograma> Cronogramas { get; set; }

    }
}


