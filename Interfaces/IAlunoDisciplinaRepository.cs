using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface IAlunoDisciplinaRepository
    {
        Task<IEnumerable<Disciplina>> ObterDisciplinasDoAluno(int alunoId);
        Task<bool> AdicionarAlunoNasDisciplinas(List<AlunoDisciplina> alunoDisciplinas);
        Task<bool> Save();  
    }
}
