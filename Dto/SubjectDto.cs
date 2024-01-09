using Gestor_Acadêmico.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Dto
{
    public class SubjectDto
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
    }
}
