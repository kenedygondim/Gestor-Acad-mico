using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface IAlunoCursoRepository
    {
        Task<IEnumerable<AlunoCurso>> ObterTodosOsAlunosCursos();
        Task<AlunoCurso> ObterAlunoCursoPeloId(int alunoId, int cursoId);
        Task<bool>  AdicionarVariosAlunosAoCurso(AlunoCurso alunoCurso);
        Task<bool> Save();
    }
}
