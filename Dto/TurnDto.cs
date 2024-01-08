using Gestor_Acadêmico.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Dto
{
    public class TurnDto
    {
        public int Id { get; set; }
        [Required]
        public string TurnCourse { get; set; } = string.Empty;
    }
}
