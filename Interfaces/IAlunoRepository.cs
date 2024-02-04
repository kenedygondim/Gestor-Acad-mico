using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> ObterTodosOsAlunos();
        Task<Aluno> ObterAlunoPeloId(int alunoId);
        Task<Aluno> ObterAlunoPelaMatricula(string matriculaDoAluno);
        Task<IEnumerable<Aluno>> ObterAlunoPeloNome(string nomeDoAluno);
        Task<IEnumerable<Nota>> ObterNotasDoAluno(int alunoId);
        Task<IEnumerable<string>> ObterTodosOsNumerosDeMatricula();
        Task<bool> CriarAluno(Aluno aluno);
        Task<bool> AtualizarAluno(Aluno aluno);
        Task<bool> DeletarAluno(Aluno aluno);
        Task<bool> Save();
    }
}
