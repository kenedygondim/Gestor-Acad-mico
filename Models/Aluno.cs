
namespace Gestor_Acadêmico.Models
{
    public class Aluno : Pessoa
    {
        public required string StatusDoAluno { get; set; }
        public decimal? IRA { get; set; }
        public int? CursoId { get; set; }
        public string? Prontuario { get ; set ; }
        public string? PeriodoDeIngresso { get ; set; }
        public Curso? Curso { get; set; }
        public IEnumerable<AlunoDisciplina>? Disciplinas { get; set; }
        public IEnumerable<Nota>? Notas { get; set; }
    }
}
