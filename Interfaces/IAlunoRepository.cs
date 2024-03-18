using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface IAlunoRepository
    {
        Task<IEnumerable<Aluno>> ObterTodosOsAlunos();
        Task<Aluno> ObterAlunoPeloId(int alunoId);
        Task<Aluno> ObterAlunoPeloProntuario(string matriculaDoAluno);
        Task<IEnumerable<Aluno>> ObterAlunoPeloNome(string nomeDoAluno);
        Task<IEnumerable<Aluno>> ObterAlunoPeloNomeComposto(string primeiroNome, string sobrenome);
        Task<IEnumerable<Nota>> ObterNotasDoAluno(int alunoId);
        Task<IEnumerable<string>> ObterProntuarios();
        Task<bool> CriarAluno(Aluno aluno);
        Task<bool> AtualizarAluno(Aluno aluno);
        Task<bool> ExcluirAluno(Aluno aluno);
        Task<bool> Save();
    }
}
