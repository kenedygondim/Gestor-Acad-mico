using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico.Interfaces
{
    public interface ICategoryCourseRepository
    {
        IEnumerable<CategoryCourse> GetCategoriesCourse();
        CategoryCourse GetCategoryCourseById(int categoryId);
        CategoryCourse GetCategoryCourseByName(string categoryName);
        bool CreateCategory(CategoryCourse categoryCourse);
        bool DeleteCategory(int categoryId);
    }
}
