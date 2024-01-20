using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Context;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Acadêmico.Repositories
{
    public class ProfessorRepository : IProfessorRepository
    {
        private readonly GestorAcademicoContext _context;

        public ProfessorRepository(GestorAcademicoContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<bool> CriarProfessor(Professor professor)
        {
            await _context.AddAsync(professor);
            return await Save();
        }

        public async Task<bool> ExcluirProfessor(Professor professor)
        {
            _context.Remove(professor);
            return await Save();
        }

        public async Task<Professor> GetProfessorPeloId(int professorId)
        {
            return await _context.Professores.Where(pro => pro.Id == professorId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Professor>> GetProfessorPeloNome(string nomeDoProfessor)
        {
            return await _context.Professores.Where(pro => pro.NomeCompleto.Contains(nomeDoProfessor)).ToListAsync();
        }

        public async Task<IEnumerable<Professor>> GetProfessores()
        {
            return await _context.Professores.ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> AtualizarProfessor(Professor professor)
        {
            _context.Update(professor);
            return await Save();
        }
    }
}
