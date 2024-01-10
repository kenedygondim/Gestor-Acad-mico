using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{
    public class CategoryCourse
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public IEnumerable<Course>? Courses { get; set; } 
    }
}
