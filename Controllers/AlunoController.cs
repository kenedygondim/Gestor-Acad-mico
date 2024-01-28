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

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AlunoDto))]
        public async Task<IActionResult> CriarAluno(Aluno aluno)
        {
            Random random = new ();

            try
            {
                if (aluno == null || !ModelState.IsValid)
                    return BadRequest("Insira os dados corretamente!");

                if (!status.Contains(aluno.StatusDoAluno, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira uma situação válida: 'Matrículado', 'Trancado', 'Formado', 'Desistente', 'Afastado'");

                if (!generos.Contains(aluno.Genero, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira um genêro válida: 'Masculino', 'Feminino', 'Outro'");

                string matriculaGerada = $"SP{random.Next(1000000, 9999999)}";
                var alunoJaExiste = await _alunoRepository.ObterAlunoPelaMatricula(matriculaGerada);

                while (alunoJaExiste != null)
                {
                    matriculaGerada = $"SP{random.Next(1000000, 9999999)}";
                    alunoJaExiste = await _alunoRepository.ObterAlunoPelaMatricula(matriculaGerada);
                }

                aluno.Matricula = matriculaGerada;

                await _alunoRepository.CriarAluno(aluno);

                var alunoCriado = await _alunoRepository.ObterAlunoPelaMatricula(aluno.Matricula);

                // FAZER AJUSTES AQUI
                ////////////////////////////////////////////////////////////////////////////////////
                var disciplinasDoCurso = await _disciplinaRepository.ObterDisciplinasDoCurso(aluno.CursoId);
                var disciplinasDoPrimeiroSemestre = disciplinasDoCurso.Where(dis => dis.SemestreDeReferencia == 1);

                if(disciplinasDoPrimeiroSemestre != null)
                {
                    foreach (var disciplina in disciplinasDoPrimeiroSemestre)
                    {
                        var alunoDisciplina = new AlunoDisciplina()
                        {
                            AlunoId = alunoCriado.Id,
                            DisciplinaId = disciplina.Id
                        };

                        var nota = new Nota()
                        {
                            AlunoId = alunoCriado.Id,
                            DisciplinaId = disciplina.Id,
                            PrimeiraAvaliacao = 0,
                            SegundaAvaliacao = 0,
                            Atividades = 0,
                            MediaGeral = 0,
                            FrequenciaDoAluno = 99,
                            NotasFechadas = false,
                            Aprovado = false
                        };


                        await _notaRepository.CriarNota(nota);

                        await _alunoDisciplinaRepository.AdicionarAlunoNaDisciplina(alunoDisciplina);
                        
                    }
                }
                /////////////////////////////////////////////////////////////////////////////////////
                
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
