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

            if (!gestorAcademicoContext.Alunos.Any())
            {
                var alunos = new List<Aluno>() {

                    new ()  { PrimeiroNome = "João Gomes", Sobrenome = "da Silva Neto", StatusDoAluno = "Matrículado", Endereco = "Rua das Palmeiras, Itaquera - SP", Cpf = "92624654564" , Genero = "Masculino", EnderecoDeEmail = "joaogomes@gmail.com", DataDeNascimento = "11/11/2000", NumeroDeTelefone = "+55 11 98765-4321"  },
                    new ()  { PrimeiroNome = "Ana", Sobrenome = "Silva Souza", StatusDoAluno = "Matrículado", Endereco = "Avenida das Flores, Vila Mariana - SP", Cpf = "78901234567", Genero = "Feminino", EnderecoDeEmail = "anasilva@gmail.com", DataDeNascimento = "05/15/1998", NumeroDeTelefone = "+55 11 95432-1678"  },
                    new () { PrimeiroNome = "Pedro", Sobrenome = "Santos Oliveira", StatusDoAluno = "Matrículado", Endereco = "Rua dos Pinheiros, Pinheiros - SP", Cpf = "65432109876", Genero = "Masculino", EnderecoDeEmail = "pedroso@gmail.com", DataDeNascimento = "08/20/1999", NumeroDeTelefone = "+55 11 91122-3445"  },
                    new () { PrimeiroNome = "Mariana", Sobrenome = "Oliveira Lima", StatusDoAluno = "Matrículado", Endereco = "Alameda das Palmas, Moema - SP", Cpf = "98765432109", Genero = "Feminino", EnderecoDeEmail = "mariana.lima@gmail.com", DataDeNascimento = "02/28/2001", NumeroDeTelefone = "+55 11 91098-7654"  },
                    new () { PrimeiroNome = "Lucas", Sobrenome= "Pereira Costa", StatusDoAluno = "Matrículado", Endereco = "Rua das Acácias, Tatuapé - SP", Cpf = "12309876543", Genero = "Masculino", EnderecoDeEmail = "lucaspc@gmail.com", DataDeNascimento = "09/10/2002", NumeroDeTelefone = "+55 11 94321-0987"  },
                    new () { PrimeiroNome = "Isabela", Sobrenome= "Oliveira Santos", StatusDoAluno = "Matrículado", Endereco = "Avenida das Rosas, Jardim Paulista - SP", Cpf = "89012345678", Genero = "Feminino", EnderecoDeEmail = "isabela.os@gmail.com", DataDeNascimento = "12/05/1997", NumeroDeTelefone = "+55 11 95678-9012"  },
                    new () { PrimeiroNome = "Rafael", Sobrenome= "Pereira Lima", StatusDoAluno = "Matrículado", Endereco = "Rua das Orquídeas, Vila Olímpia - SP", Cpf = "56789012345", Genero = "Masculino", EnderecoDeEmail = "rafaellima@gmail.com", DataDeNascimento = "03/15/2000", NumeroDeTelefone = "+55 11 99999-1111"  },
                    new () { PrimeiroNome = "Camila", Sobrenome= "Santos Almeida", StatusDoAluno = "Matrículado", Endereco = "Alameda dos Ipês, Santana - SP", Cpf = "34567890123", Genero = "Feminino", EnderecoDeEmail = "camilasa@gmail.com", DataDeNascimento = "07/22/1996", NumeroDeTelefone = "+55 11 92222-3333"  },
                    new () { PrimeiroNome = "Gabriel",Sobrenome = "Lima Oliveira", StatusDoAluno = "Matrículado", Endereco = "Rua das Violetas, Campo Belo - SP", Cpf = "23456789012", Genero = "Masculino", EnderecoDeEmail = "gabrieloliveira@gmail.com", DataDeNascimento = "01/18/1999", NumeroDeTelefone = "+55 11 97777-4444"  },
                    new () { PrimeiroNome = "Sophia", Sobrenome= "Pereira Santos", StatusDoAluno = "Matrículado", Endereco = "Avenida dos Lírios, Perdizes - SP", Cpf = "45678901234", Genero = "Feminino", EnderecoDeEmail = "sophiasantos@gmail.com", DataDeNascimento = "06/08/2001", NumeroDeTelefone = "+55 11 98888-5555"  },
                    new () { PrimeiroNome = "Matheus", Sobrenome = "Oliveira Lima", StatusDoAluno = "Matrículado", Endereco = "Rua das Hortênsias, Bela Vista - SP", Cpf = "90123456789", Genero = "Masculino", EnderecoDeEmail = "matheusolima@gmail.com", DataDeNascimento = "04/25/1997", NumeroDeTelefone = "+55 11 96666-6666"  },
                    new () { PrimeiroNome = "Larissa", Sobrenome = "Oliveira Santos", StatusDoAluno = "Matrículado", Endereco = "Rua das Camélias, Jabaquara - SP", Cpf = "76543210987", Genero = "Feminino", EnderecoDeEmail = "larissa.os@gmail.com", DataDeNascimento = "09/12/1998", NumeroDeTelefone = "+55 11 94444-4444"  },
                    new () { PrimeiroNome = "Vinícius", Sobrenome = "Silva Costa", StatusDoAluno = "Matrículado", Endereco = "Avenida das Magnólias, Santo Amaro - SP", Cpf = "54321098765", Genero = "Masculino", EnderecoDeEmail = "vinicius.costa@gmail.com", DataDeNascimento = "02/28/2000", NumeroDeTelefone = "+55 11 95555-5555"  },
                    new () { PrimeiroNome = "Juliana", Sobrenome = "Pereira Lima", StatusDoAluno = "Matrículado", Endereco = "Alameda dos Cravos, Vila Carrão - SP", Cpf = "87654321098", Genero = "Feminino", EnderecoDeEmail = "juliana.lima@gmail.com", DataDeNascimento = "06/15/1999", NumeroDeTelefone = "+55 11 98888-8888"  },
                    new () { PrimeiroNome = "Eduardo", Sobrenome = "Oliveira Silva", StatusDoAluno = "Matrículado", Endereco = "Rua das Tulipas, Mooca - SP", Cpf = "32109876543", Genero = "Masculino", EnderecoDeEmail = "eduardosilva@gmail.com", DataDeNascimento = "11/05/2001", NumeroDeTelefone = "+55 11 97777-7777"  },
                    new () { PrimeiroNome = "Matias", Sobrenome = "Lima Santos", StatusDoAluno = "Matrículado", Endereco = "Rua das Azaleias, Barra Funda - SP", Cpf = "10987654321", Genero = "Masculino", EnderecoDeEmail = "matias.santos@gmail.com", DataDeNascimento = "03/20/2002", NumeroDeTelefone = "+55 11 93333-3333"  },
                    new () { PrimeiroNome = "Amanda", Sobrenome = "Costa Oliveira", StatusDoAluno = "Matrículado", Endereco = "Alameda das Begônias, Higienópolis - SP", Cpf = "43210987654", Genero = "Feminino", EnderecoDeEmail = "amanda.oliveira@gmail.com", DataDeNascimento = "07/10/1996", NumeroDeTelefone = "+55 11 94444-1111"  },
                    new () { PrimeiroNome = "Luana", Sobrenome = "Oliveira Costa", StatusDoAluno = "Matrículado", Endereco = "Avenida das Hortênsias, Consolação - SP", Cpf = "21098765432", Genero = "Feminino", EnderecoDeEmail = "luana.costa@gmail.com", DataDeNascimento = "10/30/1995", NumeroDeTelefone = "+55 11 97777-3333"  },

                                    };

                gestorAcademicoContext.AddRange(alunos);
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
