using System.Text.RegularExpressions;
using Gestor_Acadêmico.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Gestor_Acadêmico.Context;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Gestor_Acadêmico.Validation;


namespace Gestor_Acadêmico.Validation
{
    public static class AlunoValidator 
    {
        static readonly string[] status = ["Matriculado", "Trancado", "Formado", "Desistente", "Afastado"];
        static readonly string[] generos = ["Masculino", "Feminino", "Não-binário", "Gênero fluido", "Agênero", "Bigênero", "Travesti", "Cisgênero", "Transgênero"];
        static readonly string patternCpf = @"(\d{3}\.){2}\d{3}-\d{2}";
        static readonly string patternEmail = @"([a-z0-9\.\-_]{2,})@([a-z0-9]{2,})(\.[a-z]{2,})?(\.[a-z]{2,})?(\.[a-z]{2,})";
        static readonly string patternNumeroDeTelefone = @"(\d{2})\s(\d{4,5}-\d{4})";


        public static string ObterSemestreAtual()
        {
            int anoAtual = DateTime.Now.Year;
            int mesAtual = DateTime.Now.Month;
            string semestreAtual = (mesAtual <= 6) ? "1" : "2";
            return $"{anoAtual}.{semestreAtual}";
        }

        public static string GerarMatriculaAleatoria(IEnumerable<string> matriculasEmUso)
        {
            Random random = new Random();
            string matriculaAleatoria;

            do
                matriculaAleatoria = $"SP{random.Next(1000000, 9999999)}";
            while (matriculasEmUso.Contains(matriculaAleatoria));

            return matriculaAleatoria;
        }

        public static string ObterNomeCompleto(string primeiroNome, string sobrenome) => $"{primeiroNome} {sobrenome}";
        private static bool ValidarGenero(string genero) => generos.Contains(genero);
        private static bool ValidarStatus(string status) => status.Contains(status);
        private static bool ValidarCpf(string cpf) => Regex.IsMatch(cpf, patternCpf);
        private static bool ValidarEmail(string email) => Regex.IsMatch(email, patternEmail);
        private static bool ValidarNumeroDeTelefone(string? numeroDeTelefone) => Regex.IsMatch(numeroDeTelefone, patternNumeroDeTelefone);


        public static bool ValidarCriacaoDoAluno(Aluno aluno, IEnumerable<string> matriculasEmUso , out string errorMessage)
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

            if (!ValidarCpf(aluno.Cpf))
            {
                errorMessage = "CPF inválido! Padrão desejado: XXX.XXX.XXX-XX";
                return false;
            }

            if (!ValidarEmail(aluno.EnderecoDeEmail))
            {
                errorMessage = "Endereço de e-mail inválido!";
                return false;
            }

            if (!ValidarNumeroDeTelefone(aluno.NumeroDeTelefone))
            {
                errorMessage = "Número de telefone inválido! Padrão desejado: XX XXXXX-XXXX";
                return false;
            }

            AdicionaAsInformacoes(aluno, matriculasEmUso);

            errorMessage = "";
            return true;
        }


        public static void AdicionaAsInformacoes(Aluno aluno, IEnumerable<string> matriculasEmUso)
        {
            aluno.NomeCompleto = ObterNomeCompleto(aluno.PrimeiroNome, aluno.Sobrenome);
            aluno.Matricula = GerarMatriculaAleatoria(matriculasEmUso);
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

            if (alunoAtualizado.Matricula != aluno.Matricula)
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

        public static void AtualizaAsInformacoes(Aluno aluno, Aluno alunoAtualizado)
        {
            aluno.PrimeiroNome = alunoAtualizado.PrimeiroNome;
            aluno.Sobrenome = alunoAtualizado.Sobrenome;
            aluno.NomeCompleto = AlunoValidator.ObterNomeCompleto(aluno.PrimeiroNome, aluno.Sobrenome);
            aluno.EnderecoDeEmail = alunoAtualizado.EnderecoDeEmail;
            aluno.NumeroDeTelefone = alunoAtualizado.NumeroDeTelefone;
            aluno.Endereco = alunoAtualizado.Endereco;
            aluno.Genero = alunoAtualizado.Genero;
            aluno.CursoId = alunoAtualizado.CursoId;
        }
    }


}
