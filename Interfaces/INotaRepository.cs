using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface INotaRepository
    {
        Task<IEnumerable<Nota>> ObterTodasAsNotas();
        Task<Nota> ObterNotaEspecifica(int notaId);
        Task<bool> CriarNota(Nota nota);
        Task<bool> AtualizarNota(Nota nota);
        Task<bool> Save();
    }
}
