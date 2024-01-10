using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{
    public class StudentCourse
    {
        [Required]
        public int? StudentId { get; set; }
        public Student? Student { get; set; }
        [Required]
        public int CourseId { get; set; }
        public Course? Course { get; set; } 
        
    }
}
