using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Context;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Acadêmico.Repositories
{
    public class SubjectRepository : ISubjectRepository
    {
        private readonly GestorAcademicoContext _context;

        public SubjectRepository(GestorAcademicoContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }
        public async Task<bool> CreateSubject(Subject subject)
        {
            await _context.AddAsync(subject);
            return await Save();
        }

        public async Task<Subject> GetSubjectById(int subjectId)
        {
            return await _context.Subjects.Where(cou => cou.Id == subjectId).FirstOrDefaultAsync();
        }

        public async Task<Subject> GetSubjectByName(string subjectName)
        {
            return await _context.Subjects.Where(cou => cou.SubjectName == subjectName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Subject>> GetSubjects()
        {
            return await _context.Subjects.ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateSubject(Subject subject)
        {
            _context.Update(subject);
            return await Save();
        }
    }
}
