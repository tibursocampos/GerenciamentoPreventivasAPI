using APIPreventivas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static APIPreventivas.Domain.Enum.TipoAtividadeEnum;
using APIPreventivas.Domain.Models;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace APIPreventivas.Service
{
    public class AlvoService
    {
        static private readonly APIPreventivaContext db = new APIPreventivaContext();

        static public List<Alvo> ListaAlvosCronograma(Cronograma cronograma)
        {
            var alvosDoCronograma = (List<Alvo>)from alvos in db.Alvos
                                    where alvos.IdCronograma == cronograma.IdCronograma
                                    select alvos;                       
                               
            return alvosDoCronograma;
        }     

        //atualiza status do alvo para concluído caso todas atividades estejam concluídas
        static public async Task<IActionResult> AlteraStatusAlvo (Alvo alvo)
        {
            //var cronograma = (Cronograma)from cronogramas in db.Cronogramas
            //                                    where cronogramas.IdCronograma == alvo.IdCronograma
            //                                    select cronogramas;
            if (alvo.Concluido == false)
            {
                //ListaAlvosCronograma(cronograma);
                int contAtividades = 0;
                var atividadesAlvo = (List<Atividade>)from atividades in db.Atividades
                                                      where atividades.IdAlvo == alvo.IdAlvo
                                                      select atividades;
                foreach (var atividades in atividadesAlvo)
                {
                    if (atividades.DataConclusao is null)
                    {
                        //break ;
                    }
                    else
                    {
                        contAtividades++;
                    }
                }               
                if (contAtividades == 5)
                {
                    alvo.Concluido = true;
                }
            }

            db.Alvos.Update(alvo);
            await db.SaveChangesAsync();
            return (IActionResult)alvo;
        }

        //relacionar alvo com site
        static public AlvoSite relacionaAlvoSite(Alvo alvo)
        {
            AlvoSite novoRelacionamento = new AlvoSite();

            novoRelacionamento.IdAlvo = alvo.IdAlvo;
            novoRelacionamento.IdSite = alvo.IdSite;
            
            db.AlvosSites.Add(novoRelacionamento);
            db.SaveChangesAsync();

            return novoRelacionamento;
        }
    }
}
