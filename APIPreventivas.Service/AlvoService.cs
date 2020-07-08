using APIPreventivas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIPreventivas.Service
{
    public class AlvoService
    {
        private readonly APIPreventivaContext db = new APIPreventivaContext();
        public void ProgressoCronograma (Cronograma cronogramaVerificar)
        {
            int todosAlvos = db.Alvos
                .Count(t => t.Id_cronograma == cronogramaVerificar.Id_cronograma);
            int alvosCompletos = db.Alvos
                .Where(a => a.Id_cronograma == cronogramaVerificar.Id_cronograma)
                .Count(c => c.Status == true);
        }

        private void AlvosProgramados(Alvo alvoVerificar)
        {
            int programados = db.Alvos.Where(p => (p.Id_tecnico_prog_bateria and ))
          
        }

        public void ProgamacaoDiaria (Alvo[] alvos)
        {

        }
    }
}
