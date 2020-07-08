using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace APIPreventivas.Domain.Enum
{
    public class EstadoBrEnum
    {
        public enum Estado_BR
        {
            [Description("Acre")] AC, [Description("Alagoas")] AL, [Description("Amapá")] AP, [Description("Amazonas")] AM, [Description("Bahia")] BA,
            [Description("Ceará")] CE, [Description("Distrito Federal")] DF, [Description("Espirito Santo")] ES, [Description("Goiás")] GO, [Description("Maranhão")] MA,
            [Description("Mato Grosso")] MT, [Description("Mato Grosso do Sul")] MS, [Description("Minas Gerais")] MG, [Description("Pará")] PA, [Description("Paraíba")] PB,
            [Description("Paraná")] PR, [Description("Pernambuco")] PE, [Description("Piauí")] PI, [Description("Rio de Janeiro")] RJ, [Description("Rio Grande do Norte")] RN,
            [Description("Rio Grande do Sul")] RS, [Description("Rondônia")] RO, [Description("Roraima")] RR, [Description("Santa Catarina")] SC, [Description("São Paulo")] SP,
            [Description("Sergipe")] SE, [Description("Tocantins")] TO
        }
    }
}
