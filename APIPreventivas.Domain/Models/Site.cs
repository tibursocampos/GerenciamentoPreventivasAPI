using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using static APIPreventivas.Domain.Enum.AnfMgEnum;
using static APIPreventivas.Domain.Enum.EstadoBrEnum;

namespace APIPreventivas.Models
{
    public class Site
    {                
        public string EndId { get; set; }
        public string SiteGsm { get; set; }
        public string Site3g { get; set; }
        public string SiteLte { get; set; }
        public string Cidade { get; set; }
        public EstadoBR Estado { get; set; }
        public ANFMG ANF { get; set; }
        public virtual Alvo Alvos { get; set; }
    }
}
