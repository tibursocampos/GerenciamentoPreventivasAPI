using APIPreventivas.Domain.Models;
using APIPreventivas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using static APIPreventivas.Domain.Enum.TipoAtividadeEnum;

namespace APIPreventivas.Domain.Models
{
    public class Alvo
    {
        public Alvo()
        {
            Atividades = new List<Atividade>();
            //Atividades = CriaAtividades();
            _ = new Atividade(this.IdAlvo, TipoAtividade.Aterramento);
            _ = new Atividade(this.IdAlvo, TipoAtividade.Baterias);
            _ = new Atividade(this.IdAlvo, TipoAtividade.Infraestrutura);
            _ = new Atividade(this.IdAlvo, TipoAtividade.Acesso);
            _ = new Atividade(this.IdAlvo, TipoAtividade.MW);

        }
        public int IdAlvo { get; set; }
        public int IdCronograma { get; set; }
        public int IdSite { get; set; }
        public bool Concluido { get; set; }
        public DateTime? DataConclusao { get; set; }
        public Cronograma Cronogramas { get; set; }
        public virtual List<Atividade> Atividades { get; set; }
        public ICollection<AlvoSite> AlvosSites { get; set; }

        public List<Atividade> CriaAtividades()
        {
            List<Atividade> list = new List<Atividade>();
            Atividade um = new Atividade(TipoAtividade.Aterramento);
            list.Add(um);
            Atividade dois = new Atividade(TipoAtividade.Baterias);
            list.Add(dois);
            Atividade tres = new Atividade(TipoAtividade.Infraestrutura);
            list.Add(tres);
            Atividade quatro = new Atividade(TipoAtividade.Acesso);
            list.Add(quatro);
            Atividade cinco = new Atividade(TipoAtividade.MW);
            list.Add(cinco);

            return list;
        }

    }

}