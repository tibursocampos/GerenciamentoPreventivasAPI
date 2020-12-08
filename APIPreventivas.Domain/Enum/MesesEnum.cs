using System.ComponentModel;

namespace APIPreventivas.Domain.Enum
{
    public class MesesEnum
    {
        public enum Meses 
        { 
            [Description("Janeiro")]janeiro = 1,
            [Description("Fevereiro")] fevereiro,
            [Description("Marco")] marco,
            [Description("Abril")] abril,
            [Description("Maio")] maio,
            [Description("Junho")] junho,
            [Description("Julho")] julho,
            [Description("Agosto")] agosto,
            [Description("Setembro")] setembro,
            [Description("Outrubro")] outubro,
            [Description("Novembro")] novembro,
            [Description("Dezembro")] dezembro 
        }
    }
}
