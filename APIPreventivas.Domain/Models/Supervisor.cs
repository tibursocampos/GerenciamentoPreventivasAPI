using System.Collections.Generic;
using System.ComponentModel;
using static APIPreventivas.Domain.Enum.AnfMgEnum;

namespace APIPreventivas.Models
{
    public class Supervisor : Funcionario
    {
        public ANFMG ANF { get; set; }
        public virtual ICollection<Cronograma> Cronogramas { get; set; }

    }
}


