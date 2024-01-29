using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController(
        IAlunoRepository alunoRepository,
        IAlunoDisciplinaRepository alunoDisciplinaRepository,
        IDisciplinaRepository disciplinaRepository,
        INotaRepository notaRepository,
        IMapper mapper
        ) : ControllerBase
    {
        private readonly IAlunoRepository _alunoRepository = alunoRepository;
        private readonly IAlunoDisciplinaRepository _alunoDisciplinaRepository = alunoDisciplinaRepository;
        private readonly IDisciplinaRepository _disciplinaRepository = disciplinaRepository;
        private readonly INotaRepository _notaRepository = notaRepository;
        private readonly IMapper _mapper = mapper;

        readonly string[] status = ["Matriculado", "Trancado", "Formado", "Desistente", "Afastado"];
        readonly string[] generos = ["Masculino", "Feminino", "Outro"];

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AlunoDto>))]
        public async Task<IActionResult> ObterAlunos()
        {
            try
            {
                var alunos = await _alunoRepository.ObterAlunos();
                var alunosDto = _mapper.Map<List<AlunoDto>>(alunos);
                return Ok(alunosDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar a lista de alunos");
            }
        }

        [HttpGet("{alunoId}/id")]
        [ProducesResponseType(200, Type = typeof(AlunoDto))]
        public async Task<IActionResult> ObterAlunoPeloId(int alunoId)
        {
            try
            {
                var aluno = await _alunoRepository.ObterAlunoPeloId(alunoId);

                if (aluno == null)
                    return NotFound("Aluno não encontrado");

                var alunoDto = _mapper.Map<AlunoDto>(aluno);
                return Ok(alunoDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar o aluno solicitado");
            }
        }

        [HttpGet("{alunoId}/notas")]
        [ProducesResponseType(200, Type = typeof(NotaDto))]
        public async Task<IActionResult> ObterNotasDoAluno(int alunoId)
        {
            try
            {
                var notas = await _alunoRepository.ObterNotasDoAluno(alunoId);

                if (notas == null)
                    return NotFound("Notas não encontradas");

                var notasDto = _mapper.Map<List<NotaDto>>(notas);
                return Ok(notasDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar as notas do aluno.");
            }
        }

        [HttpGet("{nomeDoAluno}")]
        [ProducesResponseType(200, Type = typeof(AlunoDto))]
        public async Task<IActionResult> ObterAlunoPeloNome(string nomeDoAluno)
        {
            try
            {
                var aluno = await _alunoRepository.ObterAlunoPeloNome(nomeDoAluno);

                if (aluno == null)
                    return NotFound("Aluno não encontrado");

                var alunoDto = _mapper.Map<List<AlunoDto>>(aluno);
                return Ok(alunoDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar o aluno solicitado");
            }
        }

        [HttpGet("{alunoId}/id/disciplinas")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DisciplinaDto>))]
        public async Task<IActionResult> ObterDisciplinasDoAluno(int alunoId)
        {
            try
            {
                var disciplinas = await _alunoDisciplinaRepository.ObterDisciplinasDoAluno(alunoId);

                if (disciplinas == null)
                    return NotFound("Não há disciplinas associadas a esse aluno.");

                var disciplinaDto = _mapper.Map<List<DisciplinaDto>>(disciplinas);
                return Ok(disciplinaDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar as disciplinas do aluno solicitado");
            }
        }

        //Código comentado para maior entendimento.
        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AlunoDto))]
        public async Task<IActionResult> CriarAluno(Aluno aluno)
        {
            Random random = new();

            //Se o usuário tentar enviar os dados em branco ou não inserir os dados corretamente, o método retornará um BadRequest.
            if (aluno == null || !ModelState.IsValid)
                return BadRequest("Insira os dados corretamente!");

            //Se o usuário tentar inserir um status ou um gênero que não exista, o método retornará um BadRequest.
            if (!status.Contains(aluno.StatusDoAluno, StringComparer.OrdinalIgnoreCase))
                return BadRequest("Insira uma situação válida: 'Matrículado', 'Trancado', 'Formado', 'Desistente', 'Afastado'");
            if (!generos.Contains(aluno.Genero, StringComparer.OrdinalIgnoreCase))
                return BadRequest("Insira um genêro válida: 'Masculino', 'Feminino', 'Outro'");

            //Primeira tentativa de gerar uma matrícula para o aluno.
            string matriculaGerada = $"SP{random.Next(1000000, 9999999)}";

            try
            {
                //Verificando se a matrícula gerada já existe no banco de dados.
                var alunoJaExiste = await _alunoRepository.ObterAlunoPelaMatricula(matriculaGerada);

                //Enquanto a matrícula gerada já existir no banco de dados, o método gerará uma nova matrícula.
                while (alunoJaExiste != null)
                {
                    matriculaGerada = $"SP{random.Next(1000000, 9999999)}";
                    alunoJaExiste = await _alunoRepository.ObterAlunoPelaMatricula(matriculaGerada);
                }

                //Quando for encontrado uma matrícula que não exista no banco de dados, a matrícula gerada será atribuída ao aluno.
                aluno.Matricula = matriculaGerada;

                //Criação do aluno
                await _alunoRepository.CriarAluno(aluno);

                //Obtendo o aluno criado
                var alunoCriado = await _alunoRepository.ObterAlunoPelaMatricula(aluno.Matricula);

                //Obtendo as disciplinas do curso do aluno
                var disciplinasDoCurso = await _disciplinaRepository.ObterDisciplinasDoCurso(alunoCriado.CursoId);

                //Filtrando as disciplinas do primeiro semestre do curso localmente
                var disciplinasDoPrimeiroSemestre = disciplinasDoCurso.Where(dis => dis.SemestreDeReferencia == 1);

                //Criando uma lista de AlunoDisciplina e uma lista de Nota para o aluno para fins de verificação de erros.
                List<AlunoDisciplina> disciplinasDoAluno = [];
                List<Nota> notasDoAluno = [];

                if(disciplinasDoPrimeiroSemestre != null)
                {
                    //Para cada disciplina do primeiro semestre do curso, será criado um objeto AlunoDisciplina e um objeto Nota.
                    foreach (var disciplina in disciplinasDoPrimeiroSemestre)
                    {
                        AlunoDisciplina alunoDisciplina = new ()
                        {
                            AlunoId = alunoCriado.Id,
                            DisciplinaId = disciplina.Id
                        };

                        Nota nota = new ()
                        {
                            AlunoId = alunoCriado.Id,
                            DisciplinaId = disciplina.Id,
                            PrimeiraAvaliacao = 0,
                            SegundaAvaliacao = 0,
                            Atividades = 0,
                            MediaGeral = 0,
                            FrequenciaDoAluno = 100,
                            NotasFechadas = false,
                            Aprovado = false
                        };


                        disciplinasDoAluno.Add(alunoDisciplina);
                        notasDoAluno.Add(nota);
                    }
                }

                //tentativa de adicionar o aluno nas disciplinas do primeiro semestre do curso.
                try
                {
                    await _alunoDisciplinaRepository.AdicionarAlunoNasDisciplinas(disciplinasDoAluno);
                }
                catch (Exception ex)
                {
                    await _alunoRepository.DeletarAluno(alunoCriado);
                    return BadRequest($"Não foi possível adicionar o aluno nas disciplinas. Aluno excluído. Erro: {ex.Message}");
                }

                //tentativa de criar as notas do aluno.
                try
                {
                    await _notaRepository.CriarNotas(notasDoAluno);
                }
                catch (Exception ex)
                {
                    await _alunoRepository.DeletarAluno(alunoCriado);
                    return BadRequest($"Não foi possível criar grade de notas do aluno. Aluno excluído. Erro: {ex.Message}");
                }
                
                //Se tudo ocorrer bem, será retornado o DTO do aluno criado.
                var alunoDto = _mapper.Map<AlunoDto>(aluno);

                return Ok(alunoDto);
            }
            catch
            {
                return BadRequest("Não foi possível adicionar aluno!");
            }
        }


        [HttpPut("{alunoId}/atualizar")]
        public async Task<IActionResult> AtualizarAluno([FromRoute] int alunoId, [FromBody] Aluno alunoAtualizado)
        {
            try
            {
                var aluno = await _alunoRepository.ObterAlunoPeloId(alunoId);

                if (aluno == null)
                    return NotFound("Aluno inexistente");

                if (!ModelState.IsValid)
                    return BadRequest("Reveja os dados inseridos");

                if (aluno.Id != alunoAtualizado.Id)
                    return BadRequest("Ocorreu um erro na validação dos identificadores.");

                if (!generos.Contains(aluno.Genero, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira um gênero válido: 'Masculino', 'Feminino, 'Outros'");

                if (!status.Contains(aluno.StatusDoAluno, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira uma situação válida: 'Matrículado', 'Trancado', 'Formado', 'Desistente', 'Afastado'");

                await _alunoRepository.AtualizarAluno(aluno);

                var alunoDto = _mapper.Map<AlunoDto>(aluno);

                return Ok("Informações alteradas com sucesso!");
            }

            catch (Exception ex) 
            {
                return BadRequest("Não foi possível editar o aluno. " + ex.Message);
            }
        }


    }
}
