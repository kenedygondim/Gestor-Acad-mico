using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{
    public abstract class Pessoa
    {
        [Key]
        public int Id { get; set; } 
        public required string PrimeiroNome { get; set; }
        public required string Sobrenome { get; set; }
        public required string CidadeDeNascimento { get; set; }
        public required string EstadoDeNascimento { get; set; }
        public required string PaisDeNascimento { get; set; }
        [Length(10, 10)]
        public required string DataDeNascimento { get; set; } 
        [Length(11, 11)]
        public required string Cpf { get; set; }
        public required string Genero { get; set; } 
        public required string CEP { get; set; }
        public required string Cidade { get; set; } 
        public required string Bairro { get; set; }
        public required string Rua { get; set; }
        public required string Numero { get; set; }
        public string? Complemento { get; set; }
        [EmailAddress]
        public required string Email { get; set; }
        [Phone]
        public string? NumeroDeTelefone { get; set; } 
    }
}

