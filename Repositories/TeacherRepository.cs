using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Context;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Acadêmico.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private readonly GestorAcademicoContext _context;

        public TeacherRepository(GestorAcademicoContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<bool> CreateTeacher(Teacher teacher)
        {
            await _context.AddAsync(teacher);
            return await Save();
        }

        public async Task<bool> DeleteTeacher(int teacherId)
        {
            var course = GetTeacherById(teacherId);
            _context.Remove(course);
            return await Save();
        }

        public async Task<Teacher> GetTeacherById(int teacherId)
        {
            return await _context.Teachers.Where(cou => cou.Id == teacherId).FirstOrDefaultAsync();
        }

        public async Task<Teacher> GetTeacherByName(string teacherName)
        {
            return await _context.Teachers.Where(cou => cou.FullName == teacherName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Teacher>> GetTeachers()
        {
            return await _context.Teachers.ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateTeacher(Teacher teacher)
        {
            _context.Update(teacher);
            return await Save();
        }
    }
}
