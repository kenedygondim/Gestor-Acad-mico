using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface IDisciplinaRepository
    {
        Task<IEnumerable<Disciplina>> GetDisciplinas();
        Task<IEnumerable<Disciplina>> GetDisciplinasDoCurso(int? cursoId);
        Task<IEnumerable<Disciplina>> GetDisciplinasDoProfessor(int professorId);
        Task<Disciplina> GetDisciplinaPeloId(int disciplinaId);
        Task<IEnumerable<Disciplina>> GetDisciplinaPeloNome(string nomeDaDisciplina);
        Task<bool> CriarDisciplina(Disciplina disciplina);
        Task<bool> AtualizarDisciplina(Disciplina disciplina);
        Task<bool> Save();
    }
}
