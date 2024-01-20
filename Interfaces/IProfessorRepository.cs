using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface IProfessorRepository
    {
        Task<IEnumerable<Professor>> GetProfessores();
        Task<Professor> GetProfessorPeloId(int professorId);
        Task<IEnumerable<Professor>> GetProfessorPeloNome (string nomeDoProfessor);
        Task<bool> CriarProfessor (Professor professor);
        Task<bool> AtualizarProfessor (Professor professor);
        Task<bool> ExcluirProfessor (Professor professor);
        Task<bool> Save();
    }
}
