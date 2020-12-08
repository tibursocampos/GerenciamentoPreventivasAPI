using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using static APIPreventivas.Domain.Enum.MesesEnum;

namespace APIPreventivas.Models
{
    public class Cronograma
    {
        public Cronograma()
        {
            Alvos = new Collection<Alvo>();
        }
        public int IdCronograma { get; set; }
        public int IdSupervisor { get; set; }
        public Meses Mes { get; set; }
        public int Ano { get; set; }
        public bool Concluido { get; set; }
        public DateTime? DataConclusao { get; set; }
        public virtual Usuario Supervisores { get; set; }
        public virtual ICollection<Alvo> Alvos { get; set; }
    }
}
