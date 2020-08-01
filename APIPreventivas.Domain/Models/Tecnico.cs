using APIPreventivas.Domain.Models;
using System.Collections.Generic;
using static APIPreventivas.Domain.Enum.AreaTecnicoEnum;

namespace APIPreventivas.Models
{
    public class Tecnico : Funcionario
    {        
        public AreaTecnico Area { get; set; } //tipo 1 para equipamento e 2 para infraestrutura
        public virtual ICollection<Atividade> Atividades { get; set; }
    }
}
