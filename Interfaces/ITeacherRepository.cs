using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface ITeacherRepository
    {
        Task<IEnumerable<Teacher>> GetTeachers();
        Task<Teacher> GetTeacherById(int teacherId);
        Task<Teacher> GetTeacherByName (string teacherName);
        Task<bool> CreateTeacher (Teacher teacher);
        Task<bool> UpdateTeacher (Teacher teacher);
        Task<bool> DeleteTeacher (int teacherId);
        Task<bool> Save();
    }
}
