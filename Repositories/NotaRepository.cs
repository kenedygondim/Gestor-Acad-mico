using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Context;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Acadêmico.Repositories
{
    public class NotaRepository : INotaRepository
    {
        private readonly GestorAcademicoContext _context; 

        public NotaRepository(GestorAcademicoContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<bool> CriarNota(Nota nota)
        {
            await _context.AddAsync(nota);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> AtualizarNota(Nota nota)
        {
            _context.Update(nota);
            return await Save();
        }

        public async Task<IEnumerable<Nota>> ObterTodasAsNotas()
        {
            return await _context.Notas.ToListAsync();
        }

        public Task<Nota> ObterNotaEspecifica(int notaId)
        {
            return _context.Notas.FirstOrDefaultAsync(n => n.Id == notaId);
        }
    }
}
