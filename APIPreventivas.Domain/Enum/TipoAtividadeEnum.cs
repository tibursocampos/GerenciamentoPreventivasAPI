using System.ComponentModel;

namespace APIPreventivas.Domain.Enum
{
    public class TipoAtividadeEnum
    {
        public enum TipoAtividade
        {
            [Description("Aterramento")] Aterramento, 
            [Description("Baterias")] Baterias, 
            [Description("Infraestrutura")] Infraestrutura, 
            [Description("Acesso")] Acesso, 
            [Description("MW")] MW
        }
    }
}
