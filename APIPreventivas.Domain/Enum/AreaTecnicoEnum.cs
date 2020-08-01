using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

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
