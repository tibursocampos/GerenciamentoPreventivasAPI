using APIPreventivas.Domain.Models;
using System.Collections.Generic;
using static APIPreventivas.Domain.Enum.AreaTecnicoEnum;

namespace APIPreventivas.Models
{
    public class Tecnico : Funcionario
    {
        
        public Area_tecnico Area { get; set; } //tipo 1 para equipamento e 2 para infraestrutura
        public ICollection<Atividade> Atividades { get; set; }
    }
}
