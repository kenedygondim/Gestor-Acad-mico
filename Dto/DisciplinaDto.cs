using Gestor_Acadêmico.Models;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Dto
{
    public class DisciplinaDto
    {
        public int Id { get; set; }
        public required string NomeDaDisciplina { get; set; } 
        public required string CodigoDaDisciplina { get; set; } 
        public required decimal CargaHoraria { get; set; }
        public required int AulasPorSemana { get; set; }
        public required int SemestreDeReferencia { get; set; }
        public required string SituacaoDaDisciplina { get; set; } 
        public int? ProfessorId { get; set; }
        public int? CursoId { get; set; }
    }
}
