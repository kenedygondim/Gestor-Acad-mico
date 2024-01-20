using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{
    public class Disciplina
    {
        [Key]
        public int Id { get; set; }

        [Length(10, 70)]
        public required string NomeDaDisciplina { get; set; } 

        [Length(5, 7)]
        //Exemplo: SPOMATI, SPOENG1
        public required string CodigoDaDisciplina { get; set; } 
        public required decimal CargaHoraria { get; set; }

        [Range(1, 10)]
        public required int AulasPorSemana { get; set; }

        public required int SemestreDeReferencia { get; set; }

        //Em aberto, Fechada, Aguardando início do semestre...
        public required string SituacaoDaDisciplina { get; set; } 
        public int? ProfessorId { get; set; }
        public Professor? Professor { get; set; }

        public int? CursoId { get; set; }
        public Curso? Curso { get; set; }

        public IEnumerable<AlunoDisciplina>? Alunos { get; set; }

        public Nota? Nota { get; set; }
    }
}
