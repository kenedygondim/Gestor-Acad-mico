using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetCourses();
        Course GetCourseById(int courseId);
        Course GetCourseByName(string courseName);
        bool CreateCourse(Course course);
        bool UpdateCourse(Course course);   
        bool DeleteCourse(int courseId);
    }
}
