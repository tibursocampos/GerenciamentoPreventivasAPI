using APIPreventivas.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;

namespace APIPreventivas.Service
{
    public class AlvoServiceAntigo
    {
        private readonly APIPreventivaContext db = new APIPreventivaContext();
        private readonly CronogramaService cronogramaAlvo = new CronogramaService();

        public void ProgressoCronograma(Cronograma cronogramaVerificar)
        {
            int todosAlvos = db.Alvos
                .Count(t => t.Id_cronograma == cronogramaVerificar.Id_cronograma);
            int alvosCompletos = db.Alvos
                .Where(a => a.Id_cronograma == cronogramaVerificar.Id_cronograma)
                .Count(c => c.Status == true);
        }

        //Retorna uma lista de objetos tipo Alvo com quais EndID estão programados para execução na data da consulta
        private List<Alvo> AlvosProgramadosHoje(Cronograma cronogramaAtual)
        {
            var alvosCronograma = cronogramaAlvo.AlvosDoCronograma(cronogramaAtual).ToList();
            //IQueryable<Alvo> alvosCronograma = db.Alvos.Where(alvos => alvos.Id_cronograma == cronogramaAtual.Id_cronograma);
            var programadosHoje = new List<Alvo>();
            foreach (Alvo alvos in alvosCronograma)
            {
                if (alvos.Data_tecnico_prog_aterramento == DateTime.Today || alvos.Data_tecnico_prog_bateria == DateTime.Today || alvos.Data_tecnico_prog_infra == DateTime.Today ||
                    alvos.Data_tecnico_prog_acesso == DateTime.Today || alvos.Data_tecnico_prog_mw == DateTime.Today)
                {
                    programadosHoje.Add(alvos);
                }
            }

            //foreach (IEnumerable<Alvo> alvos in alvosCronograma)
            //{
            //    programadosHoje += alvos.Where(p => p.Data_tecnico_prog_aterramento == DateTime.Today ||
            //                                        p.Data_tecnico_prog_bateria == DateTime.Today ||
            //                                        p.Data_tecnico_prog_infra == DateTime.Today ||
            //                                        p.Data_tecnico_prog_acesso == DateTime.Today ||
            //                                        p.Data_tecnico_prog_mw == DateTime.Today);
            //}

            return programadosHoje;
        }

        //Retorna a quantidade de alvos que tiveram alguma programação no dia anterior
        private int AlvosProgramadosDiaAnterior(Cronograma cronogramaAtual)
        {
            IQueryable alvosCronograma = cronogramaAlvo.AlvosDoCronograma(cronogramaAtual);
            //IQueryable<Alvo> alvosCronograma = db.Alvos.Where(alvos => alvos.Id_cronograma == cronogramaAtual.Id_cronograma);
            int qtdeProgramados = 0;
            foreach (IQueryable<Alvo> alvos in alvosCronograma)
            {
                qtdeProgramados += alvos.Count(p => p.Data_tecnico_prog_aterramento == DateTime.Today.AddDays(-1) ||
                                                    p.Data_tecnico_prog_bateria == DateTime.Today.AddDays(-1) ||
                                                    p.Data_tecnico_prog_infra == DateTime.Today.AddDays(-1) ||
                                                    p.Data_tecnico_prog_acesso == DateTime.Today.AddDays(-1) ||
                                                    p.Data_tecnico_prog_mw == DateTime.Today.AddDays(-1));
            }

            return qtdeProgramados;
        }

        //Retorna a quantidade de alvos que tiveram a conclusão de alguma atividade no dia anterior
        private int AlvosExecutadosDiaAnterior(Cronograma cronogramaAtual)
        {
            IQueryable alvosCronograma = cronogramaAlvo.AlvosDoCronograma(cronogramaAtual);
            //IQueryable<Alvo> alvosCronograma = db.Alvos.Where(alvos => alvos.Id_cronograma == cronogramaAtual.Id_cronograma);
            int qtdeExecutados = 0;
            foreach (IQueryable<Alvo> alvos in alvosCronograma)
            {
                qtdeExecutados += alvos.Count(p => p.Data_tecnico_exec_aterramento == DateTime.Today.AddDays(-1) ||
                                                   p.Data_tecnico_exec_bateria == DateTime.Today.AddDays(-1) ||
                                                   p.Data_tecnico_exec_infra == DateTime.Today.AddDays(-1) ||
                                                   p.Data_tecnico_exec_acesso == DateTime.Today.AddDays(-1) ||
                                                   p.Data_tecnico_exec_mw == DateTime.Today.AddDays(-1));
            }
            return qtdeExecutados;
        }

        public void ProgramadoExecutado(Cronograma cronogramaAtual)
        {
            int totalAlvosCronograma = db.Alvos.Count(total => total.Id_cronograma == cronogramaAtual.Id_cronograma);
            int programados = AlvosProgramadosDiaAnterior(cronogramaAtual);
            int executados = AlvosExecutadosDiaAnterior(cronogramaAtual);
        }

        //Programação diária onde será mostrado nome do técnico, alvo e o tipo de atividade a ser realizada.
        public void ProgamacaoDiaria(Cronograma cronogramaAtual)
        {
            var programadosHoje = AlvosProgramadosHoje(cronogramaAtual);
            Dictionary<string, string> atividade = new Dictionary<string, string>();
            atividade.Add("tec01", "Aterramento");
            atividade.Add("tec02", "Bateria");
            atividade.Add("tec02", "Infra");
            atividade.Add("tec04", "Acesso");
            atividade.Add("tec05", "MW");

            //IQueryable<Alvo> programacaoTecnicos;
            //foreach (IQueryable<Alvo> programacao in programadosHoje)

            //programacaoTecnicos = (IQueryable<Alvo>)from alvosDoDia in db.Alvos
            //                          join tecnico in db.Tecnicos on alvosDoDia.Id_tecnico_prog_aterramento equals tecnico.Id_funcionario
            //                          select new { nome = tecnico.Primeiro_nome, alvo = alvosDoDia.Site_end_id, atividade = "Aterramento" };
            var programacaoTecnicos = (List<Alvo>)(from alvosDoDia in programadosHoje
                                                   join tecnico in db.Tecnicos
                                                   on new
                                                   {
                                                       tec01 = (int)alvosDoDia.Id_tecnico_prog_aterramento,
                                                       tec02 = (int)alvosDoDia.Id_tecnico_prog_bateria,
                                                       tec03 = (int)alvosDoDia.Id_tecnico_prog_infra,
                                                       tec04 = (int)alvosDoDia.Id_tecnico_prog_acesso,
                                                       tec05 = (int)alvosDoDia.Id_tecnico_prog_mw
                                                   }
                                                   equals new
                                                   {
                                                       tec01 = tecnico.Id_funcionario,
                                                       tec02 = tecnico.Id_funcionario,
                                                       tec03 = tecnico.Id_funcionario,
                                                       tec04 = tecnico.Id_funcionario,
                                                       tec05 = tecnico.Id_funcionario
                                                   }
                                                   select new
                                                   {
                                                       nome = tecnico.Primeiro_nome,
                                                       alvo = alvosDoDia.Site_end_id,
                                                       atividade = atividade["tec01"]
                                                   });



            // var programacaoTecnicos = (List<Alvo>)from alvosDoDia in filtroNomes


        }
    }
}
