namespace Gestor_Acadêmico.Dto
{
    public record ProfessorDto
        (
            int Id,
            string PrimeiroNome,
            string Sobrenome,
            string? NomeCompleto,
            string DataDeNascimento,
            string Cpf,
            string Genero,
            string Endereco,
            string EnderecoDeEmail,
            string? NumeroDeTelefone
        );
}
