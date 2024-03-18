namespace Gestor_Acadêmico.Dto
{
    public record AlunoDto 
        (
            string PrimeiroNome,
            string Sobrenome,
            string DataDeNascimento,
            string CidadeDeNascimento,
            string EstadoDeNascimento,
            string PaisDeNascimento,
            string Cpf,
            string Cep,
            string Genero,
            string Bairro,
            string Rua,
            string Numero,
            string Complemento,
            string Cidade,
            string Email,
            string NumeroDeTelefone,
            string StatusDoAluno,
            decimal IRA,
            int CursoId,
            string Prontuario,
            string PeriodoDeIngresso
        );
}

