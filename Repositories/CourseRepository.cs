using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Context;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Acadêmico.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly GestorAcademicoContext _context;

        public CourseRepository(GestorAcademicoContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<bool> CreateCourse(Course course)
        {
            await _context.AddAsync(course);
            return await Save();
        }

        public async Task<bool> DeleteCourse(int courseId)
        {
            var course = GetCourseById(courseId);
            _context.Remove(course);
            return await Save();
        }

        public async Task<Course> GetCourseById(int courseId)
        {
            return await _context.Courses.Where(cou => cou.Id == courseId).FirstOrDefaultAsync();
        }

        public async Task<Course> GetCourseByName(string courseName)
        {
            return await _context.Courses.Where(cou => cou.CourseName == courseName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Course>> GetCourses()
        {
            return await _context.Courses.ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async  Task<bool> UpdateCourse(Course course)
        {
            _context.Update(course);
            return await Save();
        }
    }
}
