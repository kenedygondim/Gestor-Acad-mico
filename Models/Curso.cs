using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{

    public class Curso
    {
        [Key]
        public int Id { get; set; }

        [Length(10, 70)]
        public required string NomeDoCurso { get; set; }

        [Range(4, 16)]
        public required int QuantidadeDeSemestres {  get; set; }

        [DefaultValue(0)]
        public required int VagasNoPrimeiroSemestre { get; set; }

        [DefaultValue(0)]
        public required int VagasNoSegundoSemestre { get; set; }

        [DefaultValue(19)]
        public required int DuracaoDoSemestreEmSemanas { get; set; }

        [DefaultValue("Presencial")]
        public required string Modalidade { get; set; } 

        public required string Turno { get; set; } 

        public required string CategoriaDoCurso { get; set; }

        [Range(1, 8000)]
        public required decimal CargaHoraria {  get; set; }

        public IEnumerable<Disciplina>? Disciplinas { get; set; }
        public IEnumerable<Aluno>? Alunos { get; set; }
    }


}
