using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{
    public class Subject
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SubjectName { get; set; } = string.Empty;

        [Required]
        public int Hours { get; set; }

        [Required]
        public int Classes { get; set; }

        [Required]
        public int ReferencePeriod { get; set; }

        public int? TeacherId { get; set; }
        public Teacher? Teacher { get; set; }

        public int? CourseId { get; set; }
        public Course? Course { get; set; }

        public IEnumerable<StudentSubject>? Students { get; set; }

        public Grade? Grade { get; set; }
    }
}
