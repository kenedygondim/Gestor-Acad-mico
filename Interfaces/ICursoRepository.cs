using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface ICursoRepository
    {
        Task<IEnumerable<Curso>> ObterCursos();
        Task<Curso> ObterCursoPeloId(int cursoId);
        Task<IEnumerable<Curso>> ObterCursoPeloNome(string nomeDoCurso);
        Task<bool> CriarCurso(Curso curso);
        Task<bool> AtualizarCurso(Curso curso);   
        Task<bool> Save();
    }
}
