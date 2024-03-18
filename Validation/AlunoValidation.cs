using System.Text.RegularExpressions;
using Gestor_Acadêmico.Models;
using Microsoft.EntityFrameworkCore;


namespace Gestor_Acadêmico.Validation
{
    public static class AlunoValidator 
    {
        static readonly string[] status = ["Matriculado", "Trancado", "Formado", "Desistente", "Afastado"];
        static readonly string[] generos = ["Masculino", "Feminino", "Não-binário", "Gênero fluido", "Agênero", "Bigênero", "Travesti", "Cisgênero", "Transgênero"];
        static readonly string patternEmail = @"([a-z0-9\.\-_]{2,})@([a-z0-9]{2,})(\.[a-z]{2,})?(\.[a-z]{2,})?(\.[a-z]{2,})";
        static readonly string patternNumeroDeTelefone = @"(\d{2})\s(\d{4,5}-\d{4})";


        public static string ObterSemestreAtual()
        {
            int anoAtual = DateTime.Now.Year;
            int mesAtual = DateTime.Now.Month;
            string semestreAtual = (mesAtual <= 6) ? "1" : "2";
            return $"{anoAtual}.{semestreAtual}";
        }

        public static string GerarProntuarioAleatorio(IEnumerable<string> prontuariosEmUso)
        {
            Random random = new();
            string prontuarioAleatorio;

            do
                prontuarioAleatorio = $"SP{random.Next(1000000, 9999999)}";
            while (prontuariosEmUso.Contains(prontuarioAleatorio));

            return prontuarioAleatorio;
        }

        private static bool ValidarCPF(string cpf)
        {
            List<int> cpfCompletoLista = [];
            List<int> cpfNoveDigitosLista = [];

            int somaDosDigitos = 0;
            int aux = 0;
            int resto = 0;
            int digitoVerificador1 = 0;
            int digitoVerificador2 = 0;

            for (int i = 0; i < cpf.Length; i++)
                cpfCompletoLista.Add(int.Parse(cpf[i].ToString()));

            for (int j = 10; j >= 2; j--)
            {
                somaDosDigitos += cpfCompletoLista[aux] * j;
                aux++;
            }

            resto = somaDosDigitos % 11;

            if (resto < 2)
                digitoVerificador1 = 0;
            else if (resto >= 2 && resto <= 10)
                digitoVerificador1 = 11 - resto;

            for (int k = 0; k < 10; k++)
                cpfNoveDigitosLista.Add(int.Parse(cpf[k].ToString()));

            cpfNoveDigitosLista.Add(digitoVerificador1);
            aux = 0;
            somaDosDigitos = 0;
            resto = 0;

            for (int k = 11; k >= 2; k--)
            {
                somaDosDigitos += cpfNoveDigitosLista[aux] * k;
                aux++;
            }

            resto = somaDosDigitos % 11;

            if (resto < 2)
                digitoVerificador2 = 0;
            else if (resto >= 2 && resto <= 10)
                digitoVerificador2 = 11 - resto;


            return digitoVerificador1 == cpfCompletoLista[^2] && digitoVerificador2 == cpfCompletoLista[^1];
        }


        public static string ObterNomeCompleto(string primeiroNome, string sobrenome) => $"{primeiroNome} {sobrenome}";
        private static bool ValidarGenero(string genero) => generos.Contains(genero);
        private static bool ValidarStatus(string status) => status.Contains(status);
        public static bool ValidarDataDeNascimento(string dataDeNascimento) => DateTime.TryParse(dataDeNascimento, out _);
        private static bool ValidarEmail(string email) => Regex.IsMatch(email, patternEmail);
        private static bool ValidarNumeroDeTelefone(string? numeroDeTelefone) => Regex.IsMatch(numeroDeTelefone, patternNumeroDeTelefone);


        public static bool ValidarCriacaoDoAluno(Aluno aluno, IEnumerable<string> prontuariosEmUso , out string errorMessage)
        {
            if (aluno == null)
            {
                errorMessage = "Insira os dados corretamente!";
                return false;
            }

            if (!ValidarGenero(aluno.Genero))
            {
                errorMessage = $"Insira um genêro válido: {string.Join(", ", generos)}.";
                return false;
            }

            if (!ValidarStatus(aluno.StatusDoAluno))
            {
                errorMessage = $"Insira uma situação válida: {string.Join(", ", status)}.";
                return false;
            }

            if (!ValidarCPF(aluno.Cpf))
            {
                errorMessage = "Insira um CPF existente sem pontos ou traços";
                return false;
            }

            if(!ValidarDataDeNascimento(aluno.DataDeNascimento))
            {
                errorMessage = "Data de nascimento inválida! Padrão desejado: DD/MM/AAAA";
                return false;
            }

            if (!ValidarEmail(aluno.Email))
            {
                errorMessage = "Endereço de e-mail inválido!";
                return false;
            }


            if (!ValidarNumeroDeTelefone(aluno.NumeroDeTelefone) )
            {
                errorMessage = "Número de telefone inválido! Padrão desejado: XX XXXXX-XXXX";
                return false;
            }

            AdicionaAsInformacoes(aluno, prontuariosEmUso);

            errorMessage = "";
            return true;
        }


        private static void AdicionaAsInformacoes(Aluno aluno, IEnumerable<string> prontuariosEmUso)
        {
            aluno.Prontuario = GerarProntuarioAleatorio(prontuariosEmUso);               
            aluno.PeriodoDeIngresso = ObterSemestreAtual();
            aluno.IRA = 0;
        }


        public static bool ValidarAtualizacaoDoAluno(Aluno aluno, Aluno alunoAtualizado ,out string errorMessage)
        {
            if (aluno == null)
            {
                errorMessage = "Insira os dados do aluno!";
                return false;
            }

            if (aluno.Id != alunoAtualizado.Id)
            {
                errorMessage = "Ocorreu um erro na validação dos identificadores.";
                return false;
            }

            if (alunoAtualizado.Prontuario != aluno.Prontuario)
            {
                errorMessage = "O número de matrícula não pode ser alterado.";
                return false;
            }

            if (alunoAtualizado.Cpf != aluno.Cpf)
            {
                errorMessage = "O número de CPF não pode ser alterado.";
                return false;
            }

            if (alunoAtualizado.PrimeiroNome == "" || alunoAtualizado.Sobrenome == "")
            {
                errorMessage = "Preencha os campos de primeiro nome e sobrenome!";
                return false;
            }

            if (alunoAtualizado.DataDeNascimento != aluno.DataDeNascimento)
            {
                errorMessage = "Não é possível alterar a data de nascimento!";
                return false;
            }

            if (alunoAtualizado.CursoId != aluno.CursoId)
            {
                errorMessage = "Não é possível alterar o curso!";
                return false;
            }

            if (alunoAtualizado.PeriodoDeIngresso != aluno.PeriodoDeIngresso)
            {
                errorMessage = "Não é possível alterar o período de ingresso!";
                return false;
            }

            if (!ValidarGenero(aluno.Genero))
            {
                errorMessage = $"Insira um genêro válido: {string.Join(", ", generos)}.";
                return false;
            }

            if (!ValidarStatus(aluno.StatusDoAluno))
            {
                errorMessage = $"Insira uma situação válida: {string.Join(", ", status)}.";
                return false;
            }

            AtualizaAsInformacoes(aluno, alunoAtualizado);

            Console.WriteLine(aluno.PrimeiroNome);

            errorMessage = "";
            return true;
        }

        private static void AtualizaAsInformacoes(Aluno aluno, Aluno alunoAtualizado)
        {
            aluno.PrimeiroNome = alunoAtualizado.PrimeiroNome;
            aluno.Sobrenome = alunoAtualizado.Sobrenome;
            aluno.Email = alunoAtualizado.Email;
            aluno.NumeroDeTelefone = alunoAtualizado.NumeroDeTelefone;
            aluno.Genero = alunoAtualizado.Genero;
            aluno.CursoId = alunoAtualizado.CursoId;
        }
    }


}
