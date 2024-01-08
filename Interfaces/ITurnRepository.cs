using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface ITurnRepository
    {
        IEnumerable<Turn> GetTurns();
        Turn GetTurnById(int turnId);
        Turn GetTurnByName(string turnName);
        bool DeleteTurn(int turnId);
    }
}
