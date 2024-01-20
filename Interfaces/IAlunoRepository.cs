using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> GetAlunos();
        Task<Aluno> GetAlunoPeloId(int alunoId);
        Task<IEnumerable<Aluno>> GetAlunoPeloNome(string nomeDoAluno);
        Task<IEnumerable<Nota>> GetNotasDoAluno(int alunoId);
        Task<bool> CriarAluno(Aluno aluno);
        Task<bool> AtualizarAluno(Aluno aluno);
        Task<bool> Save();
    }
}
