using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{
    public class Teacher
    {

        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        public string BirthDate { get; set; } = string.Empty;

        [Required]
        public string Cpf { get; set; } = string.Empty;

        [Required]
        public string Genre { get; set; } = string.Empty;

        [Required]
        public string Address { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; } = string.Empty;

        public IEnumerable<Subject>? Subjects { get; set; }
    }
}
