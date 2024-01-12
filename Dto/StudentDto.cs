using Gestor_Acadêmico.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Dto
{
    public class StudentDto
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string Status { get; set; } = string.Empty;

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string BirthDate { get; set; } = string.Empty;

        [Required]
        [MaxLength(11)]
        [MinLength(11)]
        public string Cpf { get; set; } = string.Empty;

        [Required]
        public string Genre { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        public decimal? GPA { get; private set; }

    }
}
