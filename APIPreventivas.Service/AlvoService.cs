using APIPreventivas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static APIPreventivas.Domain.Enum.TipoAtividadeEnum;
using APIPreventivas.Domain.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIPreventivas.Service
{
    public interface IAlvoService
    {
        List<Alvo> getAlvos();
        Alvo getAlvo(int idAlvo);
        List<Alvo> GetAlvoCronograma(int idCronograma);
        Alvo PostAlvo(Alvo alvo);
        void PutAlvo(Alvo alvo);
        Alvo DeleteAlvo(int idCronograma);
        bool AlvoExists(int id);
        List<Alvo> ListaAlvosCronograma(Cronograma cronograma);
        AlvoSite RelacionaAlvoSite(Alvo alvo);
        Task<object> GetAlvosTelaAdd(int id);
    }
    public class AlvoService :IAlvoService
    {
        private readonly APIPreventivaContext db;

        public AlvoService(APIPreventivaContext context)
        {
            db = context;
        }

        public List<Alvo> getAlvos()
        {
            return db.Alvos.ToList();
        }

        public Alvo getAlvo(int idAlvo)
        {
            var alvo = db.Alvos.Find(idAlvo);
            return alvo;
        }

        public List<Alvo> GetAlvoCronograma(int idCronograma)
        {
            var alvo = db.Alvos.Where(a => a.IdCronograma == idCronograma).ToList();
            return alvo;
        }

        public Alvo PostAlvo(Alvo alvo)
        {
            alvo.Atividades = new List<Atividade>();
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

            foreach (var lista in list)
            {
                alvo.Atividades.Add(lista);
            }

            db.Alvos.Add(alvo);
            db.SaveChanges();
            RelacionaAlvoSite(alvo);

            return alvo;
        }

        public void PutAlvo(Alvo alvo)
        {
            db.Entry(alvo).State = EntityState.Modified;
            db.SaveChangesAsync();
        }

        public Alvo DeleteAlvo(int idAlvo)
        {
            var alvo = db.Alvos.Find(idAlvo);
            db.Alvos.Remove(alvo);
            db.SaveChanges();

            return alvo;
        }

        public bool AlvoExists(int id)
        {
            return db.Alvos.Any(e => e.IdAlvo == id);
        }

        public List<Alvo> ListaAlvosCronograma(Cronograma cronograma)
        {
            var alvosDoCronograma = from alvos in db.Alvos
                                    where alvos.IdCronograma == cronograma.IdCronograma
                                    select alvos;

            return alvosDoCronograma.ToList();
        }    

        //relacionar alvo com site
        public AlvoSite RelacionaAlvoSite(Alvo alvo)
        {
            AlvoSite novoRelacionamento = new AlvoSite();

            novoRelacionamento.IdAlvo = alvo.IdAlvo;
            novoRelacionamento.IdSite = alvo.IdSite;
            
            db.AlvosSites.Add(novoRelacionamento);
            db.SaveChangesAsync();

            return novoRelacionamento;
        }

        public async Task<object> GetAlvosTelaAdd(int id)
        {
            object detalhes;

            var alvosCronograma = from alvos in db.Alvos
                                  join sites in db.Sites on alvos.IdSite equals sites.IdSite
                                  where alvos.IdCronograma == id
                                  select new
                                  {
                                      alvos.IdAlvo,
                                      sites.EndId,
                                      sites.SiteGsm,
                                      sites.Site3g,                                      
                                      alvos.Concluido,
                                      alvos.DataConclusao
                                  };

            return detalhes = await alvosCronograma.ToListAsync();            
        }
    }
}
