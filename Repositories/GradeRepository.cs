using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Microsoft.EntityFrameworkCore;
using Gestor_Acadêmico.Context;

namespace Gestor_Acadêmico.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly GestorAcademicoContext _context;

        public GradeRepository(GestorAcademicoContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateGrade(Grade grade)
        {
            _context.Update(grade);
            return await Save();
        }
    }
}
