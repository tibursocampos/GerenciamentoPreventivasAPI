using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPreventivas.Models
{
    public class TecnicoAlvo
    {
        public int Id_tecnico { get; set; }
        public int Id_alvo { get; set; }
        public Alvo Alvo { get; set; }
        public Tecnico Tecnico { get; set; }
    }
}
