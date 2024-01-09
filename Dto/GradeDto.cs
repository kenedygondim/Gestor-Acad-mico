using Gestor_Acadêmico.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Dto
{
    public class GradeDto
    {
        [Key]
        public int Id { get; set; }
        public decimal Frequence { get; set; }
        public decimal FirstAvaliation { get; set; }
        public decimal SecondAvaliation { get; set; }
        public decimal Activities { get; set; }
        public decimal Balance { get; set; }
        public Subject Subject { get; set; }
    }
}
