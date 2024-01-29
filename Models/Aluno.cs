
namespace Gestor_Acadêmico.Models
{
    public class Aluno : Pessoa
    {
        public required string StatusDoAluno { get; set; }
        public required decimal IRA { get; set; }
        public required int CursoId { get; set; }
        public required string Matricula { get ; set ; }
        public required string PeriodoDeIngresso { get ; set; }
        public Curso? Curso { get; set; }
        public IEnumerable<AlunoDisciplina>? Disciplinas { get; set; }
        public IEnumerable<Nota>? Notas { get; set; }
    }
}
