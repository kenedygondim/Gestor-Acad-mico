using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{
    public class StudentSubject
    {
        [Required]
        public int SubjectId { get; set; }
        public Subject? Subject { get; set; }

        [Required]
        public int StudentId { get; set; }
        public Student? Student {  get; set; }
    }
}
