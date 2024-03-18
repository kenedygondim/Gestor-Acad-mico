
namespace Gestor_Acadêmico.Models
{
    public class Professor : Pessoa
    {
        public string? Prontuario { get; set; }
        public IEnumerable<Disciplina>? Disciplinas { get; set; }
    }
}
