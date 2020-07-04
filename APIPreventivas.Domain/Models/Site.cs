using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace APIPreventivas.Models
{
    public class Site
    {
        public enum Estado_BR {[Description("Acre")] AC,[Description("Alagoas")] AL, [Description("Amapá")] AP, [Description("Amazonas")] AM, [Description("Bahia")] BA,
            [Description("Ceará")] CE, [Description("Distrito Federal")] DF, [Description("Espirito Santo")] ES, [Description("Goiás")] GO, [Description("Maranhão")] MA,
            [Description("Mato Grosso")] MT, [Description("Mato Grosso do Sul")] MS, [Description("Minas Gerais")] MG, [Description("Pará")] PA, [Description("Paraíba")] PB,
            [Description("Paraná")] PR, [Description("Pernambuco")] PE, [Description("Piauí")] PI, [Description("Rio de Janeiro")] RJ, [Description("Rio Grande do Norte")] RN,
            [Description("Rio Grande do Sul")] RS, [Description("Rondônia")] RO, [Description("Roraima")] RR, [Description("Santa Catarina")] SC, [Description("São Paulo")] SP,
            [Description("Sergipe")] SE, [Description("Tocantins")] TO }
        public enum ANF_MG {[Description("31")] BH, [Description("32")] JF, [Description("33")] GV, [Description("34")] UR, [Description("35")] VG, [Description("37")] DV }
        public string End_id { get; set; }
        public string Site_gsm { get; set; }
        public string Site_3g { get; set; }
        public string Site_lte { get; set; }
        public string Cidade { get; set; }
        public Estado_BR Estado { get; set; }
        public ANF_MG ANF { get; set; }
        public ICollection<Alvo> Alvos { get; set; }
    }
}
