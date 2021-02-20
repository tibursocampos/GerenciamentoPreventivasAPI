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
    public class AlvoService
    {
        static private readonly APIPreventivaContext db = new APIPreventivaContext();

        static public List<Alvo> ListaAlvosCronograma(Cronograma cronograma)
        {
            var alvosDoCronograma = from alvos in db.Alvos
                                    where alvos.IdCronograma == cronograma.IdCronograma
                                    select alvos;

            return alvosDoCronograma.ToList();
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

        static public async Task<object> GetAlvosTelaAdd(int id)
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
