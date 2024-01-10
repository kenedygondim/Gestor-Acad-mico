using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{
    public class Turn
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string TurnCourse { get;  set; } = string.Empty;

        public IEnumerable<Course>? Courses { get; set; }

    }
}
 