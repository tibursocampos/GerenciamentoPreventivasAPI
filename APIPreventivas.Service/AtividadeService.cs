using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using APIPreventivas.Models;
using APIPreventivas.Domain.Models;

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

        static public IEnumerable<Atividade> AtividadesConcluidasIQueryable(Cronograma cronograma)
        {
            var alvos = AlvoService.ListaAlvosCronograma(cronograma);
            var atividadesConcluidas = (IEnumerable<Atividade>) from atividade in db.Atividades
                                       join alvo in alvos
                                       on atividade.IdAlvo equals alvo.IdAlvo
                                       where atividade.DataConclusao != null
                                       select atividade;

            return atividadesConcluidas;
        }


    }
}
