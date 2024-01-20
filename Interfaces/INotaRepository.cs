using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface INotaRepository
    {
        Task<bool> CriarNota(Nota nota);
        Task<bool> AtualizarNota(Nota nota);
        Task<bool> Save();
    }
}
