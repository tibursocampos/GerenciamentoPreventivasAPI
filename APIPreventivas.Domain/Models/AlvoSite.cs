using APIPreventivas.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace APIPreventivas.Domain.Models
{
    public class AlvoSite
    {
        public int IdAlvo { get; set; }
        public int IdSite { get; set; }
        public Alvo Alvo { get; set; }
        public Site Site { get; set; }
    }
}
