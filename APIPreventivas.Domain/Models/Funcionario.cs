﻿using static APIPreventivas.Domain.Enum.TipoUsuarioEnum;

namespace APIPreventivas.Models
{
    public abstract class Funcionario
    {        
        public int IdFuncionario { get; set; }
        public string PrimeiroNome { get; set; }
        public string UltimoNome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public Tipo_usuario Permissao { get; set; } //permissao 1 para supervisor e 2 para tecnico, de acordo com ENUM criado

    }
}
