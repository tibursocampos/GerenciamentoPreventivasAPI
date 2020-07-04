using System.Collections.Generic;

namespace APIPreventivas.Models
{
    public class Cronograma
    {
        public enum Meses { janeiro = 1, fevereiro, marco, abril, maio, junho, julho, agosto, setembro, outubro, novembro, dezembro}
        public int Id_cronograma { get; set; }
        public int Id_supervisor { get; set; }
        public Meses Mes { get; set; }
        public int Ano { get; set; }
        public bool Status { get; set; }
        public Supervisor Supervisores { get; set; }
        public ICollection<Alvo> Alvos { get; set; }
    }
}
