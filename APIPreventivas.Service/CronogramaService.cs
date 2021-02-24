using APIPreventivas.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using static APIPreventivas.Domain.Enum.MesesEnum;

namespace APIPreventivas.Service
{
    public interface ICronogramaService
    {
        List<Cronograma> GetCronogramas();
        Cronograma GetCronograma(int idCronograma);
        List<Cronograma> GetCronogramaMes(int mes);
        Cronograma PostCronograma(Cronograma cronograma);
        Cronograma DeleteCronograma(int idCronograma);
        bool CronogramaExists(int id);
        int RetornaIdCronograma(Meses mes, int ano);
        int RetornaIdCronograma(Cronograma cronograma);
        void AlteraStatusCronograma(Cronograma cronograma);
        object GetAlvosDetalhados(int id);

    }
    public class CronogramaService : ICronogramaService
    {        
        private readonly APIPreventivaContext db;
        public CronogramaService(APIPreventivaContext context)
        {
            db = context;
        }

        public List<Cronograma> GetCronogramas()
        {
            return db.Cronogramas.ToList();
        }

        public Cronograma GetCronograma(int idCronograma)
        {
            var cronograma = db.Cronogramas.Find(idCronograma);
            return cronograma;
        }

        public List<Cronograma> GetCronogramaMes(int mes)
        {
            var cronograma = db.Cronogramas.Where(c => (int)c.Mes == mes).ToList();
            return cronograma;
        }

        public Cronograma PostCronograma(Cronograma cronograma)
        {
            db.Cronogramas.Add(cronograma);
            db.SaveChanges();

            return cronograma;
        }

        public Cronograma DeleteCronograma(int idCronograma)
        {
            var cronograma = db.Cronogramas.Find(idCronograma);
            db.Cronogramas.Remove(cronograma);
            db.SaveChanges();

            return cronograma;
        }

        public bool CronogramaExists(int id)
        {
            return db.Cronogramas.Any(e => e.IdCronograma == id);
        }

        //Retorna o ID do Cronograma passando um Enum para o mes e Int para ano
        public int RetornaIdCronograma (Meses mes, int ano)
        {
            var idCronogramaBusca = from cronograma in db.Cronogramas
                               where cronograma.Mes == mes && cronograma.Ano == ano
                               select cronograma.IdCronograma;

            return idCronogramaBusca.First();
        }

        //Retorna o ID do Cronograma passando um objeto Cronograma
        public int RetornaIdCronograma(Cronograma cronograma)
        {
            var idCronogramaBusca = from cronog in db.Cronogramas
                               where cronog.IdCronograma == cronograma.IdCronograma
                               select cronog.IdCronograma;

            return idCronogramaBusca.First();
        }

        //Altera cronograma para concluído
        public void AlteraStatusCronograma(Cronograma cronograma)
        {
            db.Entry(cronograma).State = EntityState.Modified;

            if (cronograma.Concluido == false)
            {
                bool todosConcluidos = true;
                var cronogramaAlvos = db.Alvos.Where(a => a.IdCronograma == cronograma.IdCronograma).ToList();
                foreach (var alvos in cronogramaAlvos)
                { 
                    if (alvos.Concluido == false)
                    {
                        todosConcluidos = false;
                    }                
                }
                if (todosConcluidos == true)
                {
                    cronograma.Concluido = true;
                }
            }
            db.Cronogramas.Add(cronograma);
            db.SaveChanges();
        }        

        //gera a view para tela cronograma detalhado
        public object GetAlvosDetalhados(int id)
        {

            var alvosCronograma = from alvos in db.Alvos
                                  join sites in db.Sites on alvos.IdSite equals sites.IdSite
                                  join atividades in db.Atividades on alvos.IdAlvo equals atividades.IdAlvo
                                  where alvos.IdCronograma == id
                                  select new
                                  {
                                      sites.EndId,
                                      sites.SiteGsm,
                                      sites.Site3g,
                                      atividades.IdAtividade,
                                      atividades.IdTecnico,
                                      atividades.TipoAtividade,
                                      atividades.DataProgramacao,
                                      atividades.DataConclusao,
                                      alvos.Concluido
                                  };

            object detalhes = alvosCronograma.ToList();
            var novo = detalhes;
            
            return novo;
        }
    }
}
