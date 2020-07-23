﻿using System;
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
        public Estado_BR Estado { get; set; }
        public ANF_MG ANF { get; set; }
        public int IdCronograma { get; set; }
        public Cronograma Cronogramas { get; set; }
    }
}
