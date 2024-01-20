using Gestor_Acadêmico.Context;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace Gestor_Acadêmico.Models
{
    public class Aluno : Pessoa
    {
        //Exemplo: Matriculado, Desistente
        public required string StatusDoAluno { get; set; }

        private decimal _ira;
        public decimal? IRA { 
            get => _ira;
            set {
                if (Notas != null && Notas.Any())
                {
                    decimal somaNotas = Notas.Sum(g => g.MediaGeral);
                    decimal mediaNotas = somaNotas / Notas.Count();

                    _ira = mediaNotas;
                    
                    return;
                }

                _ira = 0;
            } 
        }
        public IEnumerable<AlunoCurso>? Cursos { get; set; }

        public IEnumerable<AlunoDisciplina>? Disciplinas { get; set; }

        public IEnumerable<Nota>? Notas { get; set; }


        
    }
}
