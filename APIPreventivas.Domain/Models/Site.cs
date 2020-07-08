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
        public string End_id { get; set; }
        public string Site_gsm { get; set; }
        public string Site_3g { get; set; }
        public string Site_lte { get; set; }
        public string Cidade { get; set; }
        public Estado_BR Estado { get; set; }
        public ANF_MG ANF { get; set; }
        public ICollection<Alvo> Alvos { get; set; }
    }
}
