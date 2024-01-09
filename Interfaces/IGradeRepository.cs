using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface IGradeRepository
    {
        Task<bool> UpdateGrade(Grade grade);
        Task<bool> Save();
    }
}
