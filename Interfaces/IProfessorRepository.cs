using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface IProfessorRepository
    {
        Task<IEnumerable<Professor>> ObterProfessores();
        Task<Professor> ObterProfessorPeloId(int professorId);
        //Task<IEnumerable<Professor>> ObterProfessorPeloNome (string nomeDoProfessor);
        Task<IEnumerable<string>> ObterProntuarios();
        Task<bool> CriarProfessor (Professor professor);
        Task<bool> AtualizarProfessor (Professor professor);
        Task<bool> ExcluirProfessor (Professor professor);
        Task<bool> Save();
    }
}
