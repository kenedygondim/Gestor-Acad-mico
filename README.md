## API de gerenciamento acadêmico

Olá! Desenvolvi uma API relacionada a gestão acadêmica. A API foi desenvolvida em C# com .NET Core 3.1 e Entity Framework Core. 
A API foi desenvolvida para ser consumida por um aplicativo web que será desenvolvido em React posteriormente.

## Como rodar a API

1. Clone o repositório
2. Abra o projeto no Visual Studio
3. Abra o arquivo `appsettings.json` e altere a string de conexão com o banco de dados para a sua string de conexão.
4. Certifique-se de ter o pacote "Microsoft.EntityFrameworkCore.Tools" instalado.
5. Abra o Console do Gerenciador de Pacotes do Visual Studio e execute o comando `Add-Migration GerandoDados` e depois `update-database` para criar o banco de dados.
6. Execute o projeto.

## Criação de cursos

 Para criar um curso, faça uma requisição POST para a rota `/api/curso` com o seguinte padrão:

```json
{
	"nomeDoCurso": "string",
    "quantidadeDeSemestres": 0,
    "vagasNoPrimeiroSemestre": 0,
    "vagasNoSegundoSemestre": 0,
    "duracaoDoSemestreEmSemanas": 0,
    "modalidade": "string", 
    "turno": "string", 
    "categoriaDoCurso": "string", 
    "cargaHoraria": 0 
}
```

- O campo `quantidadeDeSemestres` é a quantidade de semestres que o curso possui ao todo. 
- O campo `vagasNoPrimeiroSemestre` e `vagasNoSegundoSemestre` são a quantidade de vagas para ingresso disponíveis para cada semestre. 
- O campo `duracaoDoSemestreEmSemanas` é a quantidade de semanas que o semestre letivo possui.
- O campo `modalidade` é a modalidade do curso e deve ser escolhido entre "Presencial", "Hibrido" ou "EAD".
- O campo `turno` é o turno em que as aulas do curso ocorrem. Deve ser escolhido entre "Matutino", "Vespertino", "Noturno" ou "Integral".
- O campo `categoriaDoCurso` é a categoria do curso e deve ser escolhido entre "Bacharelado", "Licenciatura" ou "Tecnólogo".
- O campo `cargaHoraria` é a carga horária do curso.

## Criação de professores

Para criar um professor, faça uma requisição POST para a rota `/api/professor` com o seguinte padrão:

```json
{
	"primeiroNome": "string",
    "sobrenome": "string",
    "dataDeNascimento": "01/01/2000",
    "cpf": "string",
    "genero": "string",
    "endereco": "string",
    "enderecoDeEmail": "user@example.com",
    "numeroDeTelefone": "+55 11 99999-9999"
}
```

- O nome completo do aluno será gerado com base no primeiro nome e sobrenome.
- A data de nascimento deve ser no formato "dd/MM/yyyy".
- O campo CPF deve ser um número de 11 dígitos, sem pontos ou traços.
- O campo de genero deve ser escolhido entre "Masculino", "Feminino", "Não-binário", "Gênero fluido", "Agênero", "Bigênero", "Travesti", "Cisgênero" ou "Transgênero".

## Criação de disciplinas

Para criar uma disciplina, faça uma requisição POST para a rota `/api/disciplina` com o seguinte padrão:

```json
{
    "nomeDaDisciplina": "string",
    "codigoDaDisciplina": "string", 
    "cargaHoraria": 0,
    "aulasPorSemana": 0, 
    "semestreDeReferencia": 0,
    "situacaoDaDisciplina": "string", 
    "professorId": 0,
    "cursoId": 0
}
```

- O campo `codigoDaDisciplina` deve ter entre 5 e 7 caracteres. Deve servir como abreviação do nome da disciplina. Ex: "MAT2024".
- O campo `aulasPorSemana` é a quantidade de aulas que a disciplina possui por semana. E cada semestre do curso não pode exceder 25 aulas por semana.
- O campo `semestreDeReferencia` é o semestre em que a disciplina é ministrada.
- O campo `situacaoDaDisciplina` é a situação da disciplina e deve ser escolhido entre "Em andamento" ou "Fechada".

## Criação de alunos

Para criar um aluno, faça uma requisição POST para a rota `/api/aluno` com o seguinte padrão:

```json
{
	"primeiroNome": "string",
	"sobrenome": "string",
	"dataDeNascimento": "01/01/2000",
	"cpf": "12345678911",
	"genero": "string", 
	"endereco": "string",
	"enderecoDeEmail": ""
    "numeroDeTelefone": "+55 11 99999-9999",
    "cursoId": 0
```

- O nome completo do aluno será gerado com base no primeiro nome e sobrenome.
- A data de nascimento deve ser no formato "dd/MM/yyyy".
- O campo CPF deve ser um número de 11 dígitos, sem pontos ou traços.
- O campo de genero deve ser escolhido entre "Masculino", "Feminino", "Não-binário", "Gênero fluido", "Agênero", "Bigênero", "Travesti", "Cisgênero" ou "Transgênero".
- O campo `cursoId` é o Id do curso que o aluno deseja cursar.

Ao criar um aluno, o sistema automaticamente o adiciona nas disciplinas do primeiro semestre do curso escolhido. Além de ser criado a grade de notas para cada disciplina.
Após as notas se fecharem, o índice de rendimento do aluno é calculado e salvo no banco de dados.

## Sistema de rematrícula

O sistema de rematrícula é feito manualmente. O usuário deve fazer uma requisição POST para a rota `/api/rematricula` um array de "Ids" das disciplinas que deseja cursar.

No lado do cliente, com autenticações e autorizações, o sistema de rematrícula será mais claro e intuitivo. O aluno poderá ver as disciplinas disponíveis para o próximo semestre e escolher as que deseja cursar de acordo com o nome e a disponibilidade de vagas.

Por enquanto, o usuário deve enviar a lista de "Ids" das disciplinas que deseja cursar no próximo semestre.

```json
{
    [1111, 1112, 1113, 1114, 1115, 1116]
}
```

Cada disciplina será verificada se o aluno já a cursou ou não. Caso o aluno já tenha cursado a disciplina e tenha sido APROVADO, o sistema não permitirá a rematrícula.

## Sistema de notas

O sistema de notas é feito automaticamente na medida que o aluno se matricular nas disciplinas. O sistema cria uma grade de notas para cada disciplina que o aluno se matricular. O aluno pode ver suas notas e o índice de rendimento no sistema.

Com autenticações e autorizações, o professor responsável pela disciplina poderá lançar as notas do aluno no sistema, e com base nessas notas será calculado a média final e se ocorreu a aprovação.

... documentação em construção