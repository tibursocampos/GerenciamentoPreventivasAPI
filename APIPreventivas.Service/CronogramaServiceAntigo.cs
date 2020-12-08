using APIPreventivas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIPreventivas.Service
{
    public class CronogramaService
    {
        private readonly APIPreventivaContext db = new APIPreventivaContext();
        public IQueryable<Alvo> AlvosDoCronograma(Cronograma cronogramaVerificado)
        {
            var alvosSelecionados = db.Alvos.Where(a => a.Id_cronograma == cronogramaVerificado.Id_cronograma);
            return alvosSelecionados;
        }
        public Cronograma AtualizaStatusCronograma(Cronograma cronogramaVerificado)
        {
            IQueryable<Alvo> alvosSelecionados = AlvosDoCronograma(cronogramaVerificado);
            int campoVazio = 0;
            foreach (var selecionados in alvosSelecionados)

            {
                if (selecionados.Status == false)
                {
                    campoVazio++;
                }
            }

            if (campoVazio == 0)
            {
                cronogramaVerificado.Status = true;
            }

            return cronogramaVerificado;
        }
    }
}
