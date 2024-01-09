using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface ICourseRepository
    {
        Task<IEnumerable<Course>> GetCourses();
        Task<Course> GetCourseById(int courseId);
        Task<Course> GetCourseByName(string courseName);
        Task<bool> CreateCourse(Course course);
        Task<bool> UpdateCourse(Course course);   
        Task<bool> DeleteCourse(int courseId);
        Task<bool> Save();
    }
}
