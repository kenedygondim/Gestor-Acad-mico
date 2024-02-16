

using Gestor_Acadêmico.Models;
using System.Text.RegularExpressions;

namespace Gestor_Acadêmico.Validation
{
    public static class ProfessorValidation
    {
        static readonly string[] generos = ["Masculino", "Feminino", "Não-binário", "Gênero fluido", "Agênero", "Bigênero", "Travesti", "Cisgênero", "Transgênero"];
        static readonly string patternCpf = @"(\d{3}\.){2}\d{3}-\d{2}";
        static readonly string patternEmail = @"([a-z0-9\.\-_]{2,})@([a-z0-9]{2,})(\.[a-z]{2,})?(\.[a-z]{2,})?(\.[a-z]{2,})";
        static readonly string patternNumeroDeTelefone = @"(\d{2})\s(\d{4,5}-\d{4})";

        public static string ObterNomeCompleto(string primeiroNome, string sobrenome) => $"{primeiroNome} {sobrenome}";
        public static bool ValidarCpf(string cpf) => Regex.IsMatch(cpf, patternCpf);
        public static bool ValidarEmail(string email) => Regex.IsMatch(email, patternEmail);
        public static bool ValidarNumeroDeTelefone(string? numeroDeTelefone) => Regex.IsMatch(numeroDeTelefone, patternNumeroDeTelefone);
        public static bool ValidarGenero(string genero) => generos.Contains(genero);

        public static bool ValidarCriacaoDoProfessor(Professor professor, out string errorMessage)
        {
            if (professor == null)
            {
                errorMessage = "Insira os dados corretamente!";
                return false;
            }

            if (!ValidarGenero(professor.Genero))
            {
                errorMessage = $"Insira um genêro válido: {string.Join(", ", generos)}.";
                return false;
            }

            if (!ValidarCpf(professor.Cpf))
            {
                errorMessage = "CPF inválido! Padrão desejado: XXX.XXX.XXX-XX";
                return false;
            }

            if (!ValidarEmail(professor.EnderecoDeEmail))
            {
                errorMessage = "Endereço de e-mail inválido!";
                return false;
            }

            if (!ValidarNumeroDeTelefone(professor.NumeroDeTelefone))
            {
                errorMessage = "Número de telefone inválido! Padrão desejado: XX XXXXX-XXXX";
                return false;
            }

            professor.NomeCompleto = ObterNomeCompleto(professor.PrimeiroNome, professor.Sobrenome);

            errorMessage = string.Empty;
            return true;
        }

        public static bool ValidarAtualizacaoDoProfessor(Professor professor, Professor professorAtualizado ,out string errorMessage)
        {
            if (professor == null)
            {
                errorMessage = "Professor inexistente!";
                return false;
            }

            if (professor.Id != professorAtualizado.Id)
            {
                errorMessage = "Ocorreu um erro na validação dos identificadores.";
                return false;
            }

            if (professorAtualizado.Cpf != professor.Cpf)
            {
                errorMessage = "Não é possível alterar o CPF do professor.";
                return false;
            }

            if (professorAtualizado.PrimeiroNome == "" || professorAtualizado.Sobrenome == "")
            {
                errorMessage = "Preencha os campos de primeiro nome e sobrenome!";
                return false;
            }

            if (professorAtualizado.DataDeNascimento != professor.DataDeNascimento)
            {
                errorMessage = "Não é possível alterar a data de nascimento!";
                return false;
            }

            if (!ValidarGenero(professor.Genero))
            {
                errorMessage = $"Insira um genêro válido: {string.Join(", ", generos)}.";
                return false;
            }

            AtualizaAsInformacoes(professor, professorAtualizado);

            errorMessage = string.Empty;
            return true;
        }

        public static void AtualizaAsInformacoes(Professor professor, Professor professorAtualizado)
        {
            professor.PrimeiroNome = professorAtualizado.PrimeiroNome;
            professor.Sobrenome = professorAtualizado.Sobrenome;
            professor.NomeCompleto = ObterNomeCompleto(professorAtualizado.PrimeiroNome, professorAtualizado.Sobrenome);
            professor.EnderecoDeEmail = professorAtualizado.EnderecoDeEmail;
            professor.NumeroDeTelefone = professorAtualizado.NumeroDeTelefone;
            professor.Endereco = professorAtualizado.Endereco;
            professor.Genero = professorAtualizado.Genero;
        }

    }
}

