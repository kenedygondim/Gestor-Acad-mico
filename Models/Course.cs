using Gestor_Acadêmico.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{

    public class Course
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CourseName { get; set; } = string.Empty;

        [Required]
        public int Semesters {  get; set; }

        [Required]
        public int TurnId { get; set; }
        public Turn? Turn { get; set; }

        [Required]
        public int CategoryCourseId { get; set; }
        public CategoryCourse? CategoryCourse { get; set; }

        public IEnumerable<Subject>? Subjects { get; set; }
        public IEnumerable<StudentCourse>? Students { get; set; }
    }


}
