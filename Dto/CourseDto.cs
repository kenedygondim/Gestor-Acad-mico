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

        [Required]
        public int OpeningsFirstSemester { get; set; }

        [Required]
        public int OpeningsLastSemester { get; set; }

        [Required]
        public int SemesterDurationInWeeks { get; set; }

        [Required]
        public string Mode { get; set; } = string.Empty;

        [Required]
        public string Turn { get; set; } = string.Empty;

        [Required]
        public string CategoryCourse { get; set; } = string.Empty;

        [Required]
        public decimal Hours { get; set; }
    }
}

