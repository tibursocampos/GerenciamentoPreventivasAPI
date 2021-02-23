using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using APIPreventivas.Models;
using APIPreventivas.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace APIPreventivas.Service
{
    public class AtividadeService
    {
        static private readonly APIPreventivaContext db = new APIPreventivaContext();

        //static public List<Atividade> AtividadesProgramadasHoje(Cronograma cronograma)
        //{
        //    var alvos = AlvoService.ListaAlvosCronograma(cronograma);
        //    var atividadesProgramadas = from alvo in alvos
        //                                join atividade in db.Atividades
        //                                on alvo.IdAlvo equals atividade.IdAlvo
        //                                where atividade.DataProgramacao == DateTime.Now.Date
        //                                select atividade;
        //    return atividadesProgramadas.ToList();

        //}

        //retorna uma lista de objeto Atividade. Possui as atividades programadas para o dia.
        static public List<Atividade> AtividadesProgramadasHoje()
        {            
            var atividadesProgramadas = from atividade in db.Atividades
                                        where atividade.DataProgramacao == DateTime.Now.Date
                                        select atividade;

            return atividadesProgramadas.ToList();
        }

        // retorna uma lista de objeto Atividade. Possui as atividades já concluídas dentro do cronograma informado.
        static public List<Atividade> AtividadesConcluidas(Cronograma cronograma)
        {
            var alvos = AlvoService.ListaAlvosCronograma(cronograma);
            var atividadesConcluidas = from atividade in db.Atividades
                                       join alvo in alvos
                                       on atividade.IdAlvo equals alvo.IdAlvo
                                       where atividade.DataConclusao != null
                                       select atividade;                                      

            return atividadesConcluidas.ToList();
        }

        static public IEnumerable<Atividade> AtividadesConcluidasIEnumerable(Cronograma cronograma)
        {
            var alvos = AlvoService.ListaAlvosCronograma(cronograma);
            var atividadesConcluidas = (IEnumerable<Atividade>) from atividade in db.Atividades
                                       join alvo in alvos
                                       on atividade.IdAlvo equals alvo.IdAlvo
                                       where atividade.DataConclusao != null
                                       select atividade;

            return atividadesConcluidas;
        }

        //atualiza status da atividade, alvo e cronograma para concluído caso todas atividades e alvos estejam concluídos
        static public async Task AlteraStatusAlvo(Atividade atividade)
        {
            int contAtividades = 0;
            int contAlvos = 0;
            int contAlvosConcl = 0;
            int cronoAtual = 0;

            //se a atividade possui valor no campo data conclusão
            if (atividade.DataConclusao.HasValue)
            {
                //retorna as atividades relacionadas ao alvo (sempre um total de 5 atividades)
                var atividadesAlvo = await db.Atividades.Where(a => a.IdAlvo == atividade.IdAlvo).ToListAsync();
            
                foreach (var verificaAtividades in atividadesAlvo)
                {
                    if (verificaAtividades.DataConclusao.HasValue)
                    {
                        contAtividades++;
                    }
                }
            }

            //retorna o alvo que a atividade está relacionada
            var alvo = await db.Alvos.Where(a => a.IdAlvo == atividade.IdAlvo).ToListAsync();

            //retorna o cronograma que o alvo está relacionado
            //var cronogramaAtual = from alvos in db.Alvos
            //                      join crono in db.Cronogramas on alvos.IdCronograma equals crono.IdCronograma
            //                      where alvos.IdAlvo == atividade.IdAlvo
            //                      select crono;
            var cronogramaAtual = await db.Alvos.Join(db.Cronogramas,
                                        a => a.IdCronograma,
                                        c => c.IdCronograma,
                                        (a, c) => new { a, c })
                                        .Where(al => al.a.IdAlvo == atividade.IdAlvo)
                                        .Select(i => i.c).ToListAsync();

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
                var alvosCronograma = await db.Alvos.Where(a => a.IdCronograma == cronoAtual).ToListAsync();

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

            await db.SaveChangesAsync();
            //return (IActionResult)alvo;
        }
    }
}
