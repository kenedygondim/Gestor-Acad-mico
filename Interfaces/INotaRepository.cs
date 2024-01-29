using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface INotaRepository
    {
        Task<IEnumerable<Nota>> ObterTodasAsNotas();
        Task<Nota> ObterNotaEspecifica(int notaId);
        Task<bool> CriarNotas(List<Nota> notas);
        Task<bool> AtualizarNota(Nota nota);
        Task<bool> Save();
    }
}
