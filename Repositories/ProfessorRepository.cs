using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Context;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Acadêmico.Repositories
{
    public class ProfessorRepository(GestorAcademicoContext context) : IProfessorRepository
    {
        private readonly GestorAcademicoContext _context = context;

        public async Task<IEnumerable<Professor>> ObterProfessores() => await _context.Professores.ToListAsync();

        public async Task<Professor> ObterProfessorPeloId(int professorId) => await _context.Professores.FirstOrDefaultAsync(pro => pro.Id == professorId);

        public async Task<IEnumerable<Professor>> ObterProfessorPeloNome(string nomeDoProfessor) => await _context.Professores.Where(pro => pro.NomeCompleto.Contains(nomeDoProfessor)).ToListAsync();

        public async Task<bool> CriarProfessor(Professor professor)
        {
            await _context.AddAsync(professor);
            return await Save();
        }

        public async Task<bool> AtualizarProfessor(Professor professor)
        {
            _context.Update(professor);
            return await Save();
        }

        public async Task<bool> ExcluirProfessor(Professor professor)
        {
            _context.Remove(professor);
            return await Save();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
