using APIPreventivas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace APIPreventivas.Models
{
    public class Alvo
    {
        public Alvo()
        {
            Atividades = new Collection<Atividade>();
            //Sites = new Collection<Site>();
        }
        public int IdAlvo { get; set; }
        public int IdCronograma { get; set; }
        public int IdSite { get; set; }
        public bool Concluido { get; set; }
        public DateTime? DataConclusao { get; set; }
        public Cronograma Cronogramas { get; set; }
        public virtual ICollection<Atividade> Atividades { get; set; }   
        public  ICollection<AlvoSite> AlvosSites { get; set; }
        //public virtual Site Sitess { get; set; }

    }   

}