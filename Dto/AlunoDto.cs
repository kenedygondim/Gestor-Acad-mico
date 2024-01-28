using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Dto
{
    public class AlunoDto : Pessoa
    {
        public required string StatusDoAluno { get; set; } 
        public decimal? IRA { get; set; }
        public required int? CursoId { get; set; }
        public string? Matricula { get ; set; }
        public string? PeriodoDeIngresso { get ; set ; }
    }
}
