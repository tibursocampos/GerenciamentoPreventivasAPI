using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace APIPreventivas.Domain.Enum
{
    public class TipoUsuarioEnum
    {
        public enum TipoUsuario 
        {
            [Description("Supervisor")] supervisor = 1,
            [Description("Tecnico")] tecnico 
          
        }
    }
}
