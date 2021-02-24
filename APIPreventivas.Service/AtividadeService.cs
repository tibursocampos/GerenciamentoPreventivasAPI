using APIPreventivas.Domain.Models;
using APIPreventivas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIPreventivas.Service
{
    public interface IAtividadeService
    {       
        List<Atividade> AtividadesProgramadasHoje();
        List<Atividade> AtividadesConcluidas(Cronograma cronograma);
        void AlteraStatusAlvo(Atividade atividade);
        List<Atividade> AtividadesProgramadasHoje(Cronograma cronograma);
        List<Atividade> GetAtividades();
        Atividade GetAtividade(int idAtividade);
        bool AtividadeExists(int id);
        Atividade DeleteAtividade(int id);
        Atividade PostAtividade(Atividade atividade);
    }
    public class AtividadeService : IAtividadeService
    {
        private readonly APIPreventivaContext db;
        public AtividadeService(APIPreventivaContext context)
        {
            db = context;
        }
        
        public List<Atividade> GetAtividades()
        {
            return db.Atividades.ToList();
        }

        public Atividade GetAtividade(int idAtividade)
        {
            var atividade = db.Atividades.Find(idAtividade);
            return atividade;
        }

        public bool AtividadeExists(int id)
        {
            return db.Atividades.Any(e => e.IdAtividade == id);
        }

        public Atividade PostAtividade(Atividade atividade)
        {
            db.Atividades.Add(atividade);
            db.SaveChanges();

            return atividade;
        }

        public Atividade DeleteAtividade(int id)
        {
            var atividade = db.Atividades.Find(id);            

            db.Atividades.Remove(atividade);
            db.SaveChanges();

            return atividade;
        }

        public List<Atividade> AtividadesProgramadasHoje(Cronograma cronograma)
        {
            var alvos = db.Alvos.Where(a => a.IdCronograma == cronograma.IdCronograma).ToList();
            var atividadesProgramadas = from alvo in alvos
                                        join atividade in db.Atividades
                                        on alvo.IdAlvo equals atividade.IdAlvo
                                        where atividade.DataProgramacao == DateTime.Now.Date
                                        select atividade;
            return atividadesProgramadas.ToList();

        }

        //retorna uma lista de objeto Atividade. Possui as atividades programadas para o dia.
        public List<Atividade> AtividadesProgramadasHoje()
        {            
            var atividadesProgramadas = from atividade in db.Atividades
                                        where atividade.DataProgramacao == DateTime.Now.Date
                                        select atividade;

            return atividadesProgramadas.ToList();
        }

        // retorna uma lista de objeto Atividade. Possui as atividades já concluídas dentro do cronograma informado.
        public List<Atividade> AtividadesConcluidas(Cronograma cronograma)
        {
            var alvos = db.Alvos.Where(a => a.IdCronograma == cronograma.IdCronograma).ToList();
            var atividadesConcluidas = from atividade in db.Atividades
                                       join alvo in alvos
                                       on atividade.IdAlvo equals alvo.IdAlvo
                                       where atividade.DataConclusao != null
                                       select atividade;                                      

            return atividadesConcluidas.ToList();
        }

        public IEnumerable<Atividade> AtividadesConcluidasIEnumerable(Cronograma cronograma)
        {
            var alvos = db.Alvos.Where(a => a.IdCronograma == cronograma.IdCronograma).ToList();
            var atividadesConcluidas = (IEnumerable<Atividade>) from atividade in db.Atividades
                                       join alvo in alvos
                                       on atividade.IdAlvo equals alvo.IdAlvo
                                       where atividade.DataConclusao != null
                                       select atividade;

            return atividadesConcluidas;
        }

        //atualiza status da atividade, alvo e cronograma para concluído caso todas atividades e alvos estejam concluídos
        public void AlteraStatusAlvo (Atividade atividade)
        {
            int contAtividades = 0;
            int contAlvos = 0;
            int contAlvosConcl = 0;
            int cronoAtual = 0;
            db.Entry(atividade).State = EntityState.Modified;

            //se a atividade possui valor no campo data conclusão
            if (atividade.DataConclusao.HasValue)
            {
                //retorna as atividades relacionadas ao alvo (sempre um total de 5 atividades)
                var atividadesAlvo = db.Atividades.Where(a => a.IdAlvo == atividade.IdAlvo).ToList();
            
                foreach (var verificaAtividades in atividadesAlvo)
                {
                    if (verificaAtividades.DataConclusao.HasValue)
                    {
                        contAtividades++;
                    }
                }
            }

            //retorna o alvo que a atividade está relacionada
            var alvo = db.Alvos.Where(a => a.IdAlvo == atividade.IdAlvo).ToList();

            var cronogramaAtual = db.Alvos.Join(db.Cronogramas,
                                        a => a.IdCronograma,
                                        c => c.IdCronograma,
                                        (a, c) => new { a, c })
                                        .Where(al => al.a.IdAlvo == atividade.IdAlvo)
                                        .Select(i => i.c).ToList();

            if (contAtividades == 5)
            {
                foreach (var alteraAlvo in alvo)
                {
                    alteraAlvo.Concluido = true;
                    alteraAlvo.DataConclusao = atividade.DataConclusao;
                    db.Alvos.Update(alteraAlvo);
                }

                foreach (var crono in cronogramaAtual)
                {
                    cronoAtual = crono.IdCronograma;
                }

                //retorna todos os alvos relacionados ao cronograma
                var alvosCronograma = db.Alvos.Where(a => a.IdCronograma == cronoAtual).ToList();

                foreach (var alteraCrono in alvosCronograma)
                {
                    contAlvos++;
                    if (alteraCrono.Concluido)
                    {
                        contAlvosConcl++;
                    }
                }

                if(contAlvos == contAlvosConcl)
                {
                    foreach (var alteraCrono in cronogramaAtual)
                    {
                        alteraCrono.Concluido = true;
                        alteraCrono.DataConclusao = atividade.DataConclusao;
                        db.Cronogramas.Update(alteraCrono);
                    }
                }

            }
            else
            {
                foreach (var alteraAlvo in alvo)
                {
                    alteraAlvo.Concluido = false;
                    alteraAlvo.DataConclusao = null;
                    db.Alvos.Update(alteraAlvo);
                }

                foreach (var alteraCrono in cronogramaAtual)
                {
                    alteraCrono.Concluido = false;
                    alteraCrono.DataConclusao = null;
                    db.Cronogramas.Update(alteraCrono);
                }
            }                        
            
            db.SaveChanges();
            //return (IActionResult)alvo;
        }
    }
}
