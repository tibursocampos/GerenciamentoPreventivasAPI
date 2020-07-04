using System;
using System.Collections.Generic;

namespace APIPreventivas.Models
{
    public class Alvo
    {
        public int Id_alvo { get; set; }
        public int Id_cronograma { get; set; }
        public int Id_supervisor { get; set; }
        public string Site_end_id { get; set; }
        public bool Status { get; set; }
        public DateTime? Data_conclusao { get; set; }
        public int? Id_tecnico_prog_aterramento { get; set; }
        public DateTime? Data_tecnico_prog_aterramento { get; set; }
        public int? Id_tecnico_exec_aterramento { get; set; }
        public DateTime? Data_tecnico_exec_aterramento { get; set; }
        public int? Id_tecnico_prog_bateria { get; set; }
        public DateTime? Data_tecnico_prog_bateria { get; set; }
        public int? Id_tecnico_exec_bateria { get; set; }
        public DateTime? Data_tecnico_exec_bateria { get; set; }
        public int? Id_tecnico_prog_infra { get; set; }
        public DateTime? Data_tecnico_prog_infra { get; set; }
        public int? Id_tecnico_exec_infra { get; set; }
        public DateTime? Data_tecnico_exec_infra { get; set; }
        public int? Id_tecnico_prog_acesso { get; set; }
        public DateTime? Data_tecnico_prog_acesso { get; set; }
        public int? Id_tecnico_exec_acesso { get; set; }
        public DateTime? Data_tecnico_exec_acesso { get; set; }
        public int? Id_tecnico_prog_mw { get; set; }
        public DateTime? Data_tecnico_prog_mw { get; set; }
        public int? Id_tecnico_exec_mw { get; set; }
        public DateTime? Data_tecnico_exec_mw { get; set; }
        public ICollection<TecnicoAlvo> Tecnicos_alvos { get; set; }
        public Cronograma Cronogramas { get; set; }
        public Site Sites { get; set; }

    }
}
