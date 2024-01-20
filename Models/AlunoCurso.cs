using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{
    public class AlunoCurso
    {
        public required int AlunoId { get; set; }
        public Aluno? Aluno { get; set; }
        public int CursoId { get; set; }
        public Curso? Curso { get; set; } 
        
    }
}
