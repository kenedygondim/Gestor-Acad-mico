using Gestor_Acadêmico.Context;
using Gestor_Acadêmico.Models;

namespace Gestor_Acadêmico
{
    public class Seed (GestorAcademicoContext context) 
    {
        private readonly GestorAcademicoContext gestorAcademicoContext = context;

        public void SeedDataContext()
        {
           if(!gestorAcademicoContext.Cursos.Any())
            {
                var cursos = new List<Curso>() { 
                    
                    new() { NomeDoCurso = "Tecnologia em Análise e Desenvolvimento de Sistemas", Turno = "Noturno", VagasNoPrimeiroSemestre = 80, VagasNoSegundoSemestre = 40 , CargaHoraria = 2369.25m, QuantidadeDeSemestres = 6, DuracaoDoSemestreEmSemanas = 19, Modalidade = "Presencial" , CategoriaDoCurso = "Tecnólogo" }
                };

                gestorAcademicoContext.AddRange(cursos);
                gestorAcademicoContext.SaveChanges();   
            }
            if (!gestorAcademicoContext.Professores.Any())
            {
                var professores = new List<Professor>() {

                    new () {PrimeiroNome = "Maria", Sobrenome = "Oliveira Lima", Endereco = "Avenida das Flores, Moema - SP", Cpf = "78901234567", Genero = "Feminino", EnderecoDeEmail = "marialima@gmail.com", DataDeNascimento = "05/15/1975", NumeroDeTelefone = "+55 11 96580-3584" },
                    new () {PrimeiroNome = "Carlos", Sobrenome = "Silva Souza", Endereco = "Rua dos Pinheiros, Pinheiros - SP", Cpf = "65432109876", Genero = "Masculino", EnderecoDeEmail = "carlossouza@gmail.com", DataDeNascimento = "08/20/1980", NumeroDeTelefone = "+55 11 93216-5184" },
                    new () {PrimeiroNome = "Fernanda", Sobrenome = "Santos Oliveira", Endereco = "Alameda das Palmas, Moema - SP", Cpf = "98765432109", Genero = "Feminino", EnderecoDeEmail = "fernanda.oliveira@gmail.com", DataDeNascimento = "02/28/1979", NumeroDeTelefone = "+55 11 94164-1414" },
                    new () {PrimeiroNome = "Roberto", Sobrenome = "Pereira Costa", Endereco = "Rua das Acácias, Tatuapé - SP", Cpf = "12309876543", Genero = "Masculino", EnderecoDeEmail = "robertocosta@gmail.com", DataDeNascimento = "09/10/1985", NumeroDeTelefone = "+55 11 91548-4587" },
                    new () {PrimeiroNome = "Ana", Sobrenome = "Oliveira Santos", Endereco = "Avenida das Rosas, Jardim Paulista - SP", Cpf = "89012345678", Genero = "Feminino", EnderecoDeEmail = "ana.os@gmail.com", DataDeNascimento = "12/05/1972", NumeroDeTelefone = "+55 11 96548-5869" },
                    new () {PrimeiroNome = "Ricardo", Sobrenome = "Lima Oliveira", Endereco = "Rua das Orquídeas, Vila Olímpia - SP", Cpf = "56789012345", Genero = "Masculino", EnderecoDeEmail = "ricardolima@gmail.com", DataDeNascimento = "03/15/1976", NumeroDeTelefone = "+55 11 91587-8787" },
                    new () {PrimeiroNome = "Camila", Sobrenome = "Santos Almeida", Endereco = "Alameda dos Ipês, Santana - SP", Cpf = "34567890123", Genero = "Feminino", EnderecoDeEmail = "camilasa@gmail.com", DataDeNascimento = "07/22/1971", NumeroDeTelefone = "+55 11 96418-4588" },
                    new () {PrimeiroNome = "Rodrigo", Sobrenome = "Lima Oliveira", Endereco = "Rua das Violetas, Campo Belo - SP", Cpf = "23456789012", Genero = "Masculino", EnderecoDeEmail = "rodrigooliveira@gmail.com", DataDeNascimento = "01/18/1974", NumeroDeTelefone = "+55 11 92598-4848" }};

                gestorAcademicoContext.AddRange(professores);
                gestorAcademicoContext.SaveChanges();
            }
        }
    }



}
