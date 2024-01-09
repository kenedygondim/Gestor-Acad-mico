using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface ICategoryCourseRepository
    {
        Task<IEnumerable<CategoryCourse>> GetCategoriesCourse();
        Task<CategoryCourse> GetCategoryCourseById(int categoryId);
        Task<CategoryCourse> GetCategoryCourseByName(string categoryName);
        Task<bool> CreateCategory(CategoryCourse categoryCourse);
        Task<bool> DeleteCategory(int categoryId);
        Task<bool> Save();
    }
}
