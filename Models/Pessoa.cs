using System.ComponentModel.DataAnnotations;

namespace Gestor_Acadêmico.Models
{
    public abstract class Pessoa
    {
        [Key]
        public int Id { get; set; } 
        public required string PrimeiroNome { get; set; } 
        public required string Sobrenome { get; set; } 
        public required string NomeCompleto { get ; set ;}
        [Length(10, 10)]
        public required string DataDeNascimento { get; set; } 
        [Length(11, 11)]
        public required string Cpf { get; set; }
        public required string Genero { get; set; } 
        public required string Endereco { get; set; } 
        [EmailAddress]
        public required string EnderecoDeEmail { get; set; }
        [Phone]
        public string? NumeroDeTelefone { get; set; } 
    }
}
