using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface IDisciplinaRepository
    {
        Task<IEnumerable<Disciplina>> ObterDisciplinas();
        Task<IEnumerable<Disciplina>> ObterDisciplinasDoCurso(int? cursoId);
        Task<IEnumerable<Disciplina>> ObterDisciplinasDoProfessor(int professorId);
        Task<Disciplina> ObterDisciplinaPeloId(int disciplinaId);
        Task<IEnumerable<Disciplina>> ObterDisciplinaPeloNome(string nomeDaDisciplina);
        Task<bool> CriarDisciplina(Disciplina disciplina);
        Task<bool> AtualizarDisciplina(Disciplina disciplina);
        Task<bool> Save();
        Task ObterDisciplinaPeloId(int? disciplinaId);
    }
}
