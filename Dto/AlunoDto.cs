namespace Gestor_Acadêmico.Dto
{
    public record AlunoDto 
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
            string? NumeroDeTelefone,
            string StatusDoAluno,
            decimal? IRA,
            int? CursoId,
            string? Matricula,
            string? PeriodoDeIngresso
        );
}

