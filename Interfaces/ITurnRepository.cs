using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface ITurnRepository
    {
        Task<IEnumerable<Turn>> GetTurns();
        Task<Turn> GetTurnById(int turnId);
        Task<Turn> GetTurnByName(string turnName);
        Task<bool> DeleteTurn(int turnId);
        Task<bool> Save();
    }
}
