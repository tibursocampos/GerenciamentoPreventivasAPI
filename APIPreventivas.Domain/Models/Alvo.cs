using APIPreventivas.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APIPreventivas.Models
{
    public class Alvo
    {
        public int IdAlvo { get; set; }
        public int IdCronograma { get; set; }
        public bool Concluido { get; set; }
        public DateTime? DataConclusao { get; set; }
        public Cronograma Cronogramas { get; set; }
        public ICollection<Atividade> Atividades { get; set; }       

    }   

}