using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface ICursoRepository
    {
        Task<IEnumerable<Curso>> GetCursos();
        Task<Curso> GetCursoPeloId(int cursoId);
        Task<IEnumerable<Curso>> GetCursoPeloNome(string nomeDoCurso);
        Task<bool> CriarCurso(Curso curso);
        Task<bool> AtualizarCurso(Curso curso);   
        Task<bool> Save();
    }
}
