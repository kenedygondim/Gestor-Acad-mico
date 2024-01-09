using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Microsoft.EntityFrameworkCore;
using Gestor_Acadêmico.Context;

namespace Gestor_Acadêmico.Repositories
{
    public class TurnRepository : ITurnRepository
    {
        private readonly GestorAcademicoContext _context;

        public TurnRepository(GestorAcademicoContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        public async Task<bool> DeleteTurn(int turnId)
        {
            var course = GetTurnById(turnId);
            _context.Remove(course);
            return await Save();
        }

        public async Task<Turn> GetTurnById(int turnId)
        {
            return await _context.Turns.Where(tur => tur.Id == turnId).FirstOrDefaultAsync();
        }

        public async Task<Turn> GetTurnByName(string turnName)
        {
            return await _context.Turns.Where(tur => tur.TurnCourse == turnName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Turn>> GetTurns()
        {
            return await _context.Turns.ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
