using Gestor_Acadêmico.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Dto
{
    public class AlunoDto : Pessoa
    {
        public required string StatusDoAluno { get; set; } 
        public decimal? IRA { get; private set; }
    }
}
