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

        //atualiza status do alvo para concluído caso todas atividades estejam concluídas
        static public async Task<IActionResult> AlteraStatusAlvo(Atividade atividade)
        //static public Alvo AlteraStatusAlvo(Atividade atividade)
        {
            int contAtividades = 0;            
            //var cronograma = (Cronograma)from cronogramas in db.Cronogramas
            //                                    where cronogramas.IdCronograma == alvo.IdCronograma
            //                                    select cronogramas;
            if (atividade.DataConclusao.HasValue)
                {
                    //ListaAlvosCronograma(cronograma);                
                    var atividadesAlvo = from atividades in db.Atividades
                                         where atividades.IdAlvo == atividade.IdAlvo
                                         select atividades;
                    foreach (var verificaAtividades in atividadesAlvo)
                    {
                        if (verificaAtividades.DataConclusao.HasValue)
                        {
                            contAtividades++;
                        }
                    }
                }

            //var alvo = from alvoUpdate in db.Alvos
            //           where alvoUpdate.IdAlvo == atividade.IdAlvo
            //           select alvoUpdate;

            var alvo = db.Alvos.Where(a => a.IdAlvo == atividade.IdAlvo).ToListAsync();

            if (contAtividades == 5)
            {
                foreach (var alteraAlvo in await alvo)
                {
                    alteraAlvo.Concluido = true;
                    alteraAlvo.DataConclusao = atividade.DataConclusao;
                    db.Alvos.Update(alteraAlvo);
                    await db.SaveChangesAsync();
                }

            }
            else
            {
                foreach (var alteraAlvo in await alvo)
                {
                    alteraAlvo.Concluido = false;
                    alteraAlvo.DataConclusao = null;
                    db.Alvos.Update(alteraAlvo);
                    await db.SaveChangesAsync();
                }
            }
                       
            return (IActionResult)alvo;
        }


    }
}
