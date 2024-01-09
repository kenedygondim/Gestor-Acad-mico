using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Interfaces;
using Microsoft.EntityFrameworkCore;
using Gestor_Acadêmico.Context;

namespace Gestor_Acadêmico.Repositories {

    public class StudentRepository : IStudentRepository
    {

        private readonly GestorAcademicoContext _context;

        public StudentRepository(GestorAcademicoContext context)
        {
            _context = context;
            _context.Database.EnsureCreated();
        }

        public async Task<bool> CreateStudent(Student student)
        {
            await _context.AddAsync(student);
            return await Save();
        }

        public async Task<Student> GetStudentById(int studentId)
        {
            return await _context.Students.Where(stu => stu.Id == studentId).FirstOrDefaultAsync();
        }

        public async Task<Student> GetStudentByName(string studentName)
        {
            return await _context.Students.Where(stu => stu.FullName == studentName).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Student>> GetStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }

        public async Task<bool> UpdateStudent(Student student)
        {
            _context.Update(student);
            return await Save();
        }
    }
}
