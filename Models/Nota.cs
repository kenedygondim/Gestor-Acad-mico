using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{
    public class Nota
    {
        [Key]
        public int Id { get; set; }

        [Range(0,100)]
        public required decimal FrequenciaDoAluno { get; set; }

        [Range(0, 10)]
        [DefaultValue(0)]
        public required decimal PrimeiraAvaliacao { get; set; }

        [Range(0, 10)]
        [DefaultValue(0)]
        public required decimal SegundaAvaliacao { get; set; }

        [Range(0, 10)]
        [DefaultValue(0)]
        public required decimal Atividades { get; set; }

        public required int AlunoId { get; set; }

        public required int DisciplinaId { get; set; }

        [DefaultValue(false)]
        public required bool NotasFechadas { get; set; }

        [Range(0, 10)]
        public required decimal MediaGeral { get; set; }

        [DefaultValue(false)]
        public required bool Aprovado {  get; set;}

        public Aluno? Aluno { get; set; }
        public Disciplina? Disciplina { get; set; }
    }
}
