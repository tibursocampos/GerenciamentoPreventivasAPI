using APIPreventivas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using static APIPreventivas.Domain.Enum.TipoAtividadeEnum;
using APIPreventivas.Domain.Models;

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

        //static public List<Alvo> AlteraStatusAlvo(Cronograma cronograma)
        //{
        //    var concluidas = AtividadeService.AtividadesConcluidasIQueryable(cronograma);
        //    var atv1 = TipoAtividade.Aterramento;
        //    var atv2 = TipoAtividade.Baterias;
        //    var atv3 = TipoAtividade.Infraestrutura;
        //    var atv4 = TipoAtividade.Acesso;
        //    var atv5 = TipoAtividade.MW;
            

        //}
    }
}
