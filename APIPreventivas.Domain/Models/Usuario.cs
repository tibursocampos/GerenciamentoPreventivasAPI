using APIPreventivas.Domain.Models;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection.Emit;
using static APIPreventivas.Domain.Enum.AnfMgEnum;
using static APIPreventivas.Domain.Enum.AreaTecnicoEnum;
using static APIPreventivas.Domain.Enum.TipoUsuarioEnum;

namespace APIPreventivas.Models
{
    public class Usuario
    {
        public Usuario()
        {
            Cronogramas = new Collection<Cronograma>();
            Atividades = new Collection<Atividade>();
        }

        public int IdUsuario { get; set; }
        public string CPF { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }        
        public ANFMG ANF { get; set; }
        public TipoUsuario Permissao { get; set; }
        public AreaTecnico? Area { get; set; }
        public virtual ICollection<Cronograma> Cronogramas { get; set; }
        public virtual ICollection<Atividade> Atividades { get; set; }        

    }
}

