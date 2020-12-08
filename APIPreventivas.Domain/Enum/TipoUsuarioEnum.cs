using System.ComponentModel;

namespace APIPreventivas.Domain.Enum
{
    public class TipoUsuarioEnum
    {
        public enum TipoUsuario 
        {
            [Description("Admin")] admin = 1,
            [Description("Supervisor")] supervisor,
            [Description("Tecnico")] tecnico 
          
        }
    }
}
