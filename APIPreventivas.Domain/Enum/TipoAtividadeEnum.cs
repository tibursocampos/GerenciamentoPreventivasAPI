using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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
