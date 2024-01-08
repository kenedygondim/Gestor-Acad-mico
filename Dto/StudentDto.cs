using Gestor_Acadêmico.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Dto
{
    public class StudentDto
    {
        public int Id { get; set; }
        [Required]
        public string FullName { get; set; } = string.Empty;
        [Required]
        public string BirthDate { get; set; } = string.Empty;
        [Required]
        [MaxLength(11)]
        [MinLength(11)]
        public string Cpf { get; set; } = string.Empty;
        public decimal GPA { get; set; }
    }
}
