using System.ComponentModel;

namespace APIPreventivas.Domain.Enum
{
    public class AreaTecnicoEnum
    {
        public enum AreaTecnico 
        { 
            [Description("Equipamento")]equipamento = 1,
            [Description("Infraestrutura")] infraestrutura 
        }
    }
}
