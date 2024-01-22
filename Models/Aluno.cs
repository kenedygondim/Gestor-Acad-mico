using Gestor_Acadêmico.Context;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Gestor_Acadêmico.Models
{
    public class Aluno : Pessoa
    {
        //Exemplo: Matriculado, Desistente
        public required string StatusDoAluno { get; set; }
        public decimal? IRA { get; set; }
        public required int CursoId { get; set; }
        public Curso? Curso { get; set; }
        public IEnumerable<AlunoDisciplina>? Disciplinas { get; set; }
        public IEnumerable<Nota>? Notas { get; set; }
    }
}
