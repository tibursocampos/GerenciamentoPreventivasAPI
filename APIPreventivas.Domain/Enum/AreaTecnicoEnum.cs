using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace APIPreventivas.Domain.Enum
{
    public class AreaTecnicoEnum
    {
        public enum Area_tecnico 
        { 
            [Description("Equipamento")]equipamento = 1,
            [Description("Infraestrutura")] infraestrutura 
        }
    }
}
