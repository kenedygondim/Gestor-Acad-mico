using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Context;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Acadêmico.Repositories
{
    public class NotaRepository(GestorAcademicoContext context) : INotaRepository
    {
        private readonly GestorAcademicoContext _context = context;

        public async Task<IEnumerable<Nota>> ObterTodasAsNotas()
        {
            return await _context.Notas.ToListAsync();
        }

        public Task<Nota> ObterNotaEspecifica(int notaId)
        {
            return _context.Notas.FirstOrDefaultAsync(n => n.Id == notaId);
        }

        public async Task<bool> CriarNotas(List<Nota> notas)
        {
            await _context.AddRangeAsync(notas);
            return await Save();
        }

        public async Task<bool> AtualizarNota(Nota nota)
        {
            _context.Update(nota);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }        
    }
}
