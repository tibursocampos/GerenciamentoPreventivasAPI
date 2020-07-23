using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using static APIPreventivas.Domain.Enum.MesesEnum;

namespace APIPreventivas.Models
{
    public class Cronograma
    {        
        public int IdCronograma { get; set; }
        public int IdSupervisor { get; set; }
        public Meses Mes { get; set; }
        public int Ano { get; set; }
        public string IdSite { get; set; }
        public bool Concluido { get; set; }
        public DateTime? DataConclusao { get; set; }
        public Supervisor Supervisores { get; set; }
        public ICollection<Site> Sites { get; set; }
        public ICollection<Alvo> Alvos { get; set; }
    }
}
