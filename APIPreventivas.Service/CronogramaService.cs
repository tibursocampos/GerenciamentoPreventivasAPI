using APIPreventivas.Domain.Enum;
using APIPreventivas.Models;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using static APIPreventivas.Domain.Enum.MesesEnum;
using APIPreventivas.Service;

namespace APIPreventivas.Service
{
    public class CronogramaService
    {
        static private readonly APIPreventivaContext db = new APIPreventivaContext();
        
        //Retorna o ID do Cronograma passando um Enum para o mes e Int para ano
        static public int RetornaIdCronograma (Meses mes, int ano)
        {
            var idCronogramaBusca = from cronograma in db.Cronogramas
                               where cronograma.Mes == mes && cronograma.Ano == ano
                               select cronograma.IdCronograma;

            return idCronogramaBusca.First();
        }

        //Retorna o ID do Cronograma passando um objeto Cronograma
        static public int RetornaIdCronograma(Cronograma cronograma)
        {
            var idCronogramaBusca = from cronog in db.Cronogramas
                               where cronog.IdCronograma == cronograma.IdCronograma
                               select cronog.IdCronograma;

            return idCronogramaBusca.First();
        }

        //Altera cronograma para concluído
        static public Cronograma AlteraStatusCronograma(Cronograma cronograma)
        {
            if (cronograma.Concluido == false)
            {
                bool todosConcluidos = true;
                var cronogramaAlvos = AlvoService.ListaAlvosCronograma(cronograma);                
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
            return cronograma;
        }
    }
}
