## API de gerenciamento acadêmico

Olá! Desenvolvi uma API relacionada a gestão acadêmica. A API foi desenvolvida em C# com .NET Core 3.1 e Entity Framework Core. 
O serviço foi criado para ser consumido por um aplicativo web que será desenvolvido posteriormente.

## Como rodar a API

1. Clone o repositório
2. Abra o projeto no Visual Studio
3. Abra o arquivo `appsettings.json` e altere a string de conexão com o banco de dados para a sua string de conexão.
4. Certifique-se de ter o pacote "Microsoft.EntityFrameworkCore.Tools" instalado.
5. Abra o Console do Gerenciador de Pacotes do Visual Studio e execute o comando `Update-Database` para adicionar às tabelas ao banco de dados.
6. Execute o projeto com `dotnet watch run`.

## Cursos

 1 - Para criar um curso, faça uma requisição POST para a rota `/api/curso` com o seguinte padrão:

```json
{
	"nomeDoCurso": "Análise e Desenvolvimento de Sistemas",
    "quantidadeDeSemestres": 6,
    "vagasNoPrimeiroSemestre": 40,
    "vagasNoSegundoSemestre": 40,
    "duracaoDoSemestreEmSemanas": 19,
    "modalidade": "Presencial", 
    "turno": "Noturno", 
    "categoriaDoCurso": "Técnologo", 
    "cargaHoraria": 2000 
}
```

- O campo `quantidadeDeSemestres` é a quantidade de semestres que o curso possui ao todo. 
- O campo `vagasNoPrimeiroSemestre` e `vagasNoSegundoSemestre` são a quantidade de vagas para ingresso disponíveis para cada semestre. 
- O campo `duracaoDoSemestreEmSemanas` é a quantidade de semanas que o semestre letivo possui.
- O campo `modalidade` é a modalidade do curso e deve ser escolhido entre "Presencial", "Hibrido" ou "EAD".
- O campo `turno` é o turno em que as aulas do curso ocorrem. Deve ser escolhido entre "Matutino", "Vespertino", "Noturno" ou "Integral".
- O campo `categoriaDoCurso` é a categoria do curso e deve ser escolhido entre "Bacharelado", "Licenciatura" ou "Tecnólogo".
- O campo `cargaHoraria` é a carga horária do curso em horas.

2 - Para listar todos os cursos, faça uma requisição GET para a rota `/api/curso`.

3 - Para listar um curso específico, faça uma requisição GET para a rota `/api/curso/{id}`.

4 - Para listar cursos pelo nome, faça uma requisição GET para a rota `/api/curso/{nomeDoCurso}`.

5 - Para atualizar um curso, faça uma requisição PUT para a rota `/api/curso/{id}/atualizar` com o seguinte padrão de corpo:

```json
{
	"id": 1,
	"nomeDoCurso": "Análise e Desenvolvimento de Sistemas",
	"quantidadeDeSemestres": 6,
	"vagasNoPrimeiroSemestre": 40,
	"vagasNoSegundoSemestre": 40,
	"duracaoDoSemestreEmSemanas": 19,
	"modalidade": "Presencial", 
	"turno": "Noturno", 
	"categoriaDoCurso": "Técnologo", 
	"cargaHoraria": 2000 
}
```

## Professores

1 - Para criar um professor, faça uma requisição POST para a rota `/api/professor` com o seguinte padrão:

```json
{
    "primeiroNome": "Marcos",
    "sobrenome": "Mariano Mezenga",
    "dataDeNascimento": "01/04/1982",
    "cidadeDeNascimento": "Maracanaú",
    "estadoDeNascimento": "Ceará",
    "paisDeNascimento": "Brasil",
    "cpf": "04444564789",
    "cep": "09999990",
    "genero": "Masculino",
    "bairro": "Vila Mariana",
    "rua": "Rua das Flores",
    "numero": "182",
    "complemento": "B",
    "cidade": "São Paulo",
    "email": "mezenga82@example.com",
    "numeroDeTelefone": "+55 11 99999-9999"
}
```

- `dataDeNascimento` deve ser no formato "dd/MM/yyyy". Qualquer data de nascimento que seja diferente desse padrão será considerada inválida.
- O campo `cpf` deve ser um número de 11 dígitos, no padrão "00000000000", sem pontos ou traços. Esse campo possui verificação de CPF válidos, ou seja, CPFs que não seguem as regras da Receita Federal.
- O campo de `genero` deve ser escolhido entre "Masculino", "Feminino", "Não-binário", "Gênero fluido", "Agênero", "Bigênero", "Travesti", "Cisgênero" ou "Transgênero".
- O campo `cep` deve ser um número de 8 dígitos, somente dígitos.
- O campo `complemento` pode ser nulo.
- O campo `email` possui regras de validação, permitindo até 4 domínios pós '@'.
- O campo do `numeroDeTelefone` deve, preferencialmente, seguir o padrão internacional de números de telefone. Ex: "+55 11 99999-9999".

O prontuário do professor é gerado automaticamente pelo sistema. O prontuário é composto por "SP" + Número entre 100000 e 999999 + "P". Ex: "SP456541P".

2 - Para listar todos os professores, faça uma requisição GET para a rota `/api/professor`.

3 - Para listar um professor específico, faça uma requisição GET para a rota `/api/professor/{id}`.

4 - Para listar professores pelo nome, faça uma requisição GET para a rota `/api/professor/{nomeDoProfessor}`.

5 - Para atualizar um professor, faça uma requisição PUT para a rota `/api/professor/{id}/atualizar` com o seguinte padrão de corpo:

```json
{
	"id": 1,
	"primeiroNome": "Marcos",
	"sobrenome": "Mariano Mezenga",
	"dataDeNascimento": "01/04/1982",
	"cidadeDeNascimento": "Maracanaú",
	"estadoDeNascimento": "Ceará",
	"paisDeNascimento": "Brasil",
	"cpf": "04444564789",
	"cep": "09999990",
	"genero": "Masculino",
	"bairro": "Vila Mariana",
	"rua": "Rua das Flores",
	"numero": "182",
	"complemento": "B",
	"cidade": "São Paulo",
	"email": "mezenga82@example.com",
    "numeroDeTelefone": "+55 11 99999-9999"
}
```

Lembrando que alguns dados são imutáveis, como o CPF, data de nascimento, período de ingresso e o prontuário do professor. Mantenha esses campos inalterados.


6 - Para deletar um professor, faça uma requisição DELETE para a rota `/api/professor/{id}/excluir`.



## Disciplinas

1 - Para criar uma disciplina, faça uma requisição POST para a rota `/api/disciplina` com o seguinte padrão:

```json
{
    "nomeDaDisciplina": "Lógica de Programação",
    "codigoDaDisciplina": "LOGPRO1", 
    "cargaHoraria": 80,
    "aulasPorSemana": 5, 
    "semestreDeReferencia": 1,
    "situacaoDaDisciplina": "Em andamento", 
    "professorId": 1,
    "cursoId": 1
}
```

- O campo `codigoDaDisciplina` deve ter entre 5 e 7 caracteres. Deve servir como abreviação do nome da disciplina. Ex: "MAT2024".
- O campo `aulasPorSemana` é a quantidade de aulas que a disciplina possui por semana. E cada semestre do curso não pode exceder 25 aulas por semana.
- O campo `semestreDeReferencia` é o semestre em que a disciplina é ministrada.
- O campo `situacaoDaDisciplina` é a situação da disciplina e deve ser escolhido entre "Em andamento" ou "Fechada".
- O campo `professorId` é o Id do professor que ministrará a disciplina.
- O campo `cursoId` é o Id do curso que a disciplina pertence.

2 - Para listar todas as disciplinas da instituição de ensino, faça uma requisição GET para a rota `/api/disciplina`.

3 - Para listar as disciplinas de um curso específico, faça uma requisição GET para a rota `/api/disciplina/{cursoId}/curso/disciplinas`.

4 - Para listar uma disciplina específica, faça uma requisição GET para a rota `/api/disciplina/{disciplinaId}/id`.

5 - Para listar disciplinas pelo nome, faça uma requisição GET para a rota `/api/disciplina/{nomeDaDisciplina}`.

6 - Para listar as disciplinas que um professor ministra, faça uma requisição GET para a rota `/api/disciplina/{professorId}/professor/disciplinas`.

## Alunos

1 - Para criar um aluno, faça uma requisição POST para a rota `/api/aluno` com o seguinte padrão:

```json
{
	"primeiroNome": "Marcos",
    "sobrenome": "Mariano Mezenga",
    "dataDeNascimento": "01/04/1982",
    "cidadeDeNascimento": "Maracanaú",
    "estadoDeNascimento": "Ceará",
    "paisDeNascimento": "Brasil",
    "cpf": "04444564789",
    "cep": "09999990",
    "genero": "Masculino",
    "bairro": "Vila Mariana",
    "rua": "Rua das Flores",
    "numero": "182",
    "complemento": "B",
    "cidade": "São Paulo",
    "email": "mezenga82@example.com",
    "numeroDeTelefone": "+55 11 99999-9999"
    "cursoId": 1,
    "statusDoAluno": "Matriculado"
}
```

- `dataDeNascimento` deve ser no formato "dd/MM/yyyy". Qualquer data de nascimento que seja diferente desse padrão será considerada inválida.
- O campo `cpf` deve ser um número de 11 dígitos, no padrão "00000000000", sem pontos ou traços. Esse campo possui verificação de CPF válidos, ou seja, CPFs que não seguem as regras da Receita Federal.
- O campo de `genero` deve ser escolhido entre "Masculino", "Feminino", "Não-binário", "Gênero fluido", "Agênero", "Bigênero", "Travesti", "Cisgênero" ou "Transgênero".
- O campo `cep` deve ser um número de 8 dígitos, somente dígitos.
- O campo `complemento` pode ser nulo.
- O campo `email` possui regras de validação, permitindo até 4 domínios pós '@'.
- O campo do `numeroDeTelefone` deve, preferencialmente, seguir o padrão internacional de números de telefone. Ex: "+55 11 99999-9999".
- O campo `cursoId` é o Id do curso que o aluno deseja cursar.
- O campo `statusDoAluno` é o status do aluno e deve ser escolhido entre "Matriculado", "Trancado", "Desistente" ou "Formado".

O prontuário do aluno é gerado automaticamente pelo sistema. O prontuário é composto por "SP" + Número entre 1000000 e 9999999. Ex: "SP4565414".

Ao criar um aluno, o sistema automaticamente o adiciona nas disciplinas do primeiro semestre do curso escolhido. Além de ser criado a grade de notas para cada disciplina.
Após as notas se fecharem, o índice de rendimento do aluno é calculado e salvo no banco de dados.

2 - Para listar todos os alunos da instituição de ensino, faça uma requisição GET para a rota `/api/aluno`.

3 - Para listar um aluno específico, faça uma requisição GET para a rota `/api/aluno/{id}`.

4 - Para listar alunos pelo nome, faça uma requisição GET para a rota `/api/aluno/{nomeDoAluno}`.

5 - Para listar as disciplinas que um aluno está matriculado ou já cursou anteriormente, faça uma requisição GET para a rota `/api/aluno/{alunoId}/disciplinas`.

6 - Para consultar as notas de um aluno, faça uma requisição GET para a rota `/api/aluno/{alunoId}/notas`.

7 - Para atualizar um aluno, faça uma requisição PUT para a rota `/api/aluno/{id}/atualizar` com o seguinte padrão de corpo:

```json
{
	"id": 1,
	"primeiroNome": "Marcos",
	"sobrenome": "Mariano Mezenga",
	"dataDeNascimento": "01/04/1982",
	"cidadeDeNascimento": "Maracanaú",
	"estadoDeNascimento": "Ceará",
	"paisDeNascimento": "Brasil",
	"cpf": "04444564789",
	"cep": "09999990",
	"genero": "Masculino",
	"bairro": "Vila Mariana",
	"rua": "Rua das Flores",
	"numero": "182",
	"complemento": "B",
	"cidade": "São Paulo",
	"email": "mezenga82@example.com",
    "numeroDeTelefone": "+55 11 99999-9999"
    "statusDoAluno": "Formado"

}
```

Lembrando que alguns dados são imutáveis, como o CPF, data de nascimento, período de ingresso e o prontuário do aluno. Mantenha esses campos inalterados.

8 - Para deletar um aluno, faça uma requisição DELETE para a rota `/api/aluno/{id}/excluir`.

## Notas

Para alterar as notas de um aluno, faça uma requisição PUT para a rota `/api/nota/{notaId}/atualizar` com o seguinte padrão:

```json
{
    "id": 5018, 
    "frequenciaDoAluno": 100,
    "primeiraAvaliacao": 10,
    "segundaAvaliacao": 7,
    "atividades": 5,
    "alunoId": 1,
    "disciplinaId": 1,
    "notasFechadas": false,
}
```

- O campo `frequenciaDoAluno` é a frequência do aluno na disciplina que varia de 0 a 100.
- O campo `primeiraAvaliacao` é a nota da primeira avaliação do aluno na disciplina que varia de 0 a 10.
- O campo `segundaAvaliacao` é a nota da segunda avaliação do aluno na disciplina que varia de 0 a 10.
- O campo `atividades` é a nota das atividades do aluno na disciplina que varia de 0 a 10.
- O campo `alunoId` é o Id do aluno.
- O campo `disciplinaId` é o Id da disciplina em que a grade de nota está associada.
- O campo `notasFechadas` é um booleano que indica se as notas da disciplina estão fechadas ou não.

Ao inserir as notas, o sistema automaticamente calcula a média final do aluno e verifica se ele foi aprovado ou não. O sistema também atualizará o índice de rendimento do aluno após o fechamento das notas.

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
