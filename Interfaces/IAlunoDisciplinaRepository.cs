using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface IAlunoDisciplinaRepository
    {
        Task<IEnumerable<Disciplina>> ObterDisciplinasDoAluno(int alunoId);
        Task<bool> AdicionarAlunoNaDisciplina(AlunoDisciplina alunoDisciplina);
        Task<bool> Save();  
    }
}
