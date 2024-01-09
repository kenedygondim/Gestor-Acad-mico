using Gestor_Acadêmico.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Dto
{
    public class CourseDto
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CourseName { get; set; } = string.Empty;
        [Required]
        public int Semesters { get; set; }
        public Turn Turn { get; set; }
        public CategoryCourse CategoryCourse { get; set; }
    }
}

