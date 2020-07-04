using System.Collections.Generic;

namespace APIPreventivas.Models
{
    public class Tecnico : Funcionario
    {
        public enum Area_tecnico { equipamento = 1, infraestrutura }
        public Area_tecnico Area { get; set; }
        public ICollection<TecnicoAlvo> Tecnicos_alvos { get; set; }
    }
}
