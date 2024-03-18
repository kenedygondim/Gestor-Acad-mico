

using Gestor_Acadêmico.Models;
using Microsoft.Extensions.WebEncoders.Testing;
using System.Text.RegularExpressions;

namespace Gestor_Acadêmico.Validation
{
    public static class ProfessorValidation
    {
        static readonly string[] generos = ["Masculino", "Feminino", "Não-binário", "Gênero fluido", "Agênero", "Bigênero", "Travesti", "Cisgênero", "Transgênero"];
        static readonly string patternCpf = @"^[0-9]{11}$";
        static readonly string patternEmail = @"([a-z0-9\.\-_]{2,})@([a-z0-9]{2,})(\.[a-z]{2,})?(\.[a-z]{2,})?(\.[a-z]{2,})";
        static readonly string patternNumeroDeTelefone = @"(\d{2})\s(\d{4,5}-\d{4})";

        public static string ObterNomeCompleto(string primeiroNome, string sobrenome) => $"{primeiroNome} {sobrenome}";
        public static bool ValidarCpf(string cpf) => Regex.IsMatch(cpf, patternCpf);
        public static bool ValidarEmail(string email) => Regex.IsMatch(email, patternEmail);
        public static bool ValidarNumeroDeTelefone(string? numeroDeTelefone) => Regex.IsMatch(numeroDeTelefone, patternNumeroDeTelefone);
        public static bool ValidarGenero(string genero) => generos.Contains(genero);
        public static bool ValidarDataDeNascimento(string dataDeNascimento) => DateTime.TryParse(dataDeNascimento, out _);
        public static string GerarProntuarioAleatorio(IEnumerable<string> prontuariosEmUso)
        {
            Random random = new ();
            string prontuarioAleatorio;

            do
                prontuarioAleatorio = $"SP{random.Next(100000, 999999)}P";
            while (prontuariosEmUso.Contains(prontuarioAleatorio));

            return prontuarioAleatorio;
        }

        public static bool ValidarCriacaoDoProfessor(Professor professor, IEnumerable<string> prontuariosEmUso, out string errorMessage)
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

            if (!ValidarDataDeNascimento(professor.DataDeNascimento))
            {
                errorMessage = "Data de nascimento inválida! Padrão desejado: DD/MM/AAAA";
                return false;
            }

            if (!ValidarCpf(professor.Cpf))
            {
                errorMessage = "CPF inválido! Insira apenas os números do CPF.";
                return false;
            }

            if (!ValidarEmail(professor.Email))
            {
                errorMessage = "Endereço de e-mail inválido!";
                return false;
            }

            if (!ValidarNumeroDeTelefone(professor.NumeroDeTelefone))
            {
                errorMessage = "Número de telefone inválido! Padrão desejado: XX XXXXX-XXXX";
                return false;
            }

           professor.Prontuario = GerarProntuarioAleatorio(prontuariosEmUso);

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

            if (professor.Prontuario != professorAtualizado.Prontuario)
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
            //professor.NomeCompleto = ObterNomeCompleto(professorAtualizado.PrimeiroNome, professorAtualizado.Sobrenome);
            //professor.EnderecoDeEmail = professorAtualizado.EnderecoDeEmail;
            professor.NumeroDeTelefone = professorAtualizado.NumeroDeTelefone;
            //professor.Endereco = professorAtualizado.Endereco;
            professor.Genero = professorAtualizado.Genero;
        }

    }
}

