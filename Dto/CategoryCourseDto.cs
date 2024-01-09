using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Dto
{
    public class CategoryCourseDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
    }
}
