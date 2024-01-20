using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{
    public class Professor : Pessoa
    {
        public IEnumerable<Disciplina>? Disciplinas { get; set; }
    }
}
