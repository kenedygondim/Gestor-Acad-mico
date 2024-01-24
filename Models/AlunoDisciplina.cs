
namespace Gestor_Acadêmico.Models
{
    public class AlunoDisciplina
    {
        public required int DisciplinaId { get; set; }
        public Disciplina? Disciplina { get; set; }
        public required int AlunoId { get; set; }
        public Aluno? Aluno {  get; set; }
    }
}
