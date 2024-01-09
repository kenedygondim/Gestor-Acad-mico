using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface ISubjectRepository
    {
        Task<IEnumerable<Subject>> GetSubjects();
        Task<Subject> GetSubjectById(int id);
        Task<Subject> GetSubjectByName(string name);
        Task<bool> CreateSubject(Subject subject);
        Task<bool> UpdateSubject(Subject subject);
        Task<bool> Save();
    }
}
