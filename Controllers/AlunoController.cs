using AutoMapper;
using Gestor_Acadêmico.Context;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Gestor_Acadêmico.Validation;
using System.Collections.Generic;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController(
        GestorAcademicoContext context,
        IAlunoRepository alunoRepository,
        IAlunoDisciplinaRepository alunoDisciplinaRepository,
        IDisciplinaRepository disciplinaRepository,
        INotaRepository notaRepository,
        IMapper mapper
        ) : ControllerBase 
    {
        private readonly GestorAcademicoContext _context = context;
        private readonly IAlunoRepository _alunoRepository = alunoRepository;
        private readonly IAlunoDisciplinaRepository _alunoDisciplinaRepository = alunoDisciplinaRepository;
        private readonly IDisciplinaRepository _disciplinaRepository = disciplinaRepository;
        private readonly INotaRepository _notaRepository = notaRepository;
        private readonly IMapper _mapper = mapper;
        private static readonly char[] separator = [' '];

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AlunoDto>))]
        public async Task<IActionResult> ObterAlunos()
        {
            try
            {
                IEnumerable<Aluno> alunos = await _alunoRepository.ObterTodosOsAlunos();
                if (alunos is null)
                {
                    return NotFound("Nenhum aluno encontrado");
                }
                IEnumerable<AlunoDto> alunosDto = _mapper.Map<IEnumerable<AlunoDto>>(alunos);
                return Ok(alunosDto);
            }
            catch (SqlException ex)
            {
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        [HttpGet("{alunoId}/id")]
        [ProducesResponseType(200, Type = typeof(AlunoDto))]
        public async Task<IActionResult> ObterAlunoPeloId([FromRoute] int alunoId)
        {
            try
            {
                Aluno aluno = await _alunoRepository.ObterAlunoPeloId(alunoId);
                if (aluno is null)
                {
                    return NotFound("Aluno não encontrado.");
                }
                AlunoDto alunoDto = _mapper.Map<AlunoDto>(aluno);
                return Ok(alunoDto);
            }
            catch (SqlException ex)
            {
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        [HttpGet("{nomeDoAluno}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AlunoDto>))]
        public async Task<IActionResult> ObterAlunoPeloNome([FromRoute] string nomeDoAluno)
        {
            IEnumerable<Aluno> aluno = new List<Aluno>();


            try
            {
                if (nomeDoAluno.Contains(' '))
                {
                    string[] partesDoNome = nomeDoAluno.Split(separator, 2);
                    string primeiroNome = partesDoNome[0];
                    string sobrenomes = partesDoNome[1];
                    List<string> nomes = [.. nomeDoAluno.Split(' ')];

                    aluno = await _alunoRepository.ObterAlunoPeloNomeComposto(nomes[0], nomes[1]);
                }
                else
                {
                    aluno = await _alunoRepository.ObterAlunoPeloNome(nomeDoAluno);
                }

                if (aluno is null)
                {
                    return NotFound("Aluno não encontrado");
                }
                IEnumerable<AlunoDto> alunoDto = _mapper.Map<IEnumerable<AlunoDto>>(aluno);
                return Ok(alunoDto);
            }
            catch(SqlException ex)
            {
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch(Exception ex)
            {
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        [HttpGet("{alunoId}/disciplinas")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DisciplinaDto>))]
        public async Task<IActionResult> ObterDisciplinasDoAluno([FromRoute] int alunoId)
        {
            try
            {
                IEnumerable<Disciplina> disciplinas = await _alunoDisciplinaRepository.ObterDisciplinasDoAluno(alunoId);
                if (disciplinas is null)
                {
                    return NotFound("Não há disciplinas associadas a esse aluno.");
                }      
                IEnumerable<DisciplinaDto> disciplinaDto = _mapper.Map<List<DisciplinaDto>>(disciplinas);
                return Ok(disciplinaDto);
            }
            catch(SqlException ex)
            {
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch(Exception ex)
            {
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        [HttpGet("{alunoId}/notas")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<NotaDto>))]
        public async Task<IActionResult> ObterNotasDoAluno([FromRoute] int alunoId)
        {
            try
            {
                IEnumerable<Nota> notas = await _alunoRepository.ObterNotasDoAluno(alunoId);
                if (notas is null)
                {
                    return NotFound("Notas não encontradas");
                }
                IEnumerable<NotaDto> notasDto = _mapper.Map<IEnumerable<NotaDto>>(notas);
                return Ok(notasDto);
            }
            catch (SqlException ex)
            {
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AlunoDto))]
        public async Task<IActionResult> CriarAluno([FromBody] Aluno aluno)
        {
            if (!ModelState.IsValid)
                return BadRequest("Insira os dados corretamente!");

            using var dbContextTransaction = _context.Database.BeginTransaction();

            try
            {
                IEnumerable<string> prontuariosEmUso = await _alunoRepository.ObterProntuarios();

                if (!AlunoValidator.ValidarCriacaoDoAluno(aluno, prontuariosEmUso, out string errorMessage))
                {
                    return BadRequest(errorMessage);
                }

                await _alunoRepository.CriarAluno(aluno);          
                Aluno alunoCriado = await _alunoRepository.ObterAlunoPeloProntuario(aluno.Prontuario);

                IEnumerable<Disciplina> disciplinasDoCurso = await _disciplinaRepository.ObterDisciplinasDoCurso((int) alunoCriado.CursoId);
                IEnumerable<Disciplina> disciplinasDoPrimeiroSemestre = disciplinasDoCurso.Where(dis => dis.SemestreDeReferencia == 1).ToList();

                List<AlunoDisciplina> disciplinasEscolhidasPeloAluno = [];
                List<Nota> listaDeNotasDoAlunoDasDisciplinasEscolhidas = [];
                
                if (!disciplinasDoPrimeiroSemestre.Any()) 
                    return BadRequest("Não foi possível obter as disciplinas do primeiro semestre do curso.");

                foreach (var disciplina in disciplinasDoPrimeiroSemestre)
                {
                    AlunoDisciplina alunoDisciplina = new()
                    {
                        AlunoId = alunoCriado.Id,
                        DisciplinaId = disciplina.Id
                    };

                    Nota nota = new()
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

                    disciplinasEscolhidasPeloAluno.Add(alunoDisciplina);
                    listaDeNotasDoAlunoDasDisciplinasEscolhidas.Add(nota);
                }

                await Task.WhenAll(
                 _alunoDisciplinaRepository.AdicionarAlunoNasDisciplinas(disciplinasEscolhidasPeloAluno),
                 _notaRepository.CriarNotas(listaDeNotasDoAlunoDasDisciplinasEscolhidas)
                 );

                dbContextTransaction.Commit();

                AlunoDto alunoDto = _mapper.Map<AlunoDto>(aluno);

                return Ok(alunoDto);
            }
            catch (SqlException ex)
            {
                dbContextTransaction.Rollback();
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                dbContextTransaction.Rollback();
                return BadRequest($"Ocorreu problemas com uma das operações: {ex.Message}");
            }
            catch (Exception ex)
            { 
                dbContextTransaction.Rollback();
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
               
        }


        [HttpPost("{alunoId}/rematricular")]
        [ProducesResponseType(200, Type = typeof(List<Disciplina>))]
        public async Task<IActionResult> RematriculaDoAluno([FromRoute] int alunoId, [FromBody] List<int> disciplinasIds)
        {
            using var dbContextTransaction = _context.Database.BeginTransaction();
            try
            {
                Aluno aluno = await _alunoRepository.ObterAlunoPeloId(alunoId);

                if (aluno is null)
                {
                    return NotFound("Aluno inexistente");
                }
                if (aluno.PeriodoDeIngresso == AlunoValidator.ObterSemestreAtual())
                {
                    return BadRequest("O aluno não pode ser rematriculado, pois está no primeiro período do curso.");
                }   
                if (aluno.StatusDoAluno is not "Matriculado")
                {
                    return BadRequest("Somente alunos devidamente matriculados podem realizar o processo de rematrícula.");
                }

                IEnumerable<Disciplina> disciplinasAnteriores = await _alunoDisciplinaRepository.ObterDisciplinasDoAluno(alunoId);
                IEnumerable<Nota> notasDoAluno = await _alunoRepository.ObterNotasDoAluno(alunoId);

                List<AlunoDisciplina> disciplinasDesejadas = [];
                List<Nota> gradeDeNotas = [];

                foreach (var disciplinaId in disciplinasIds)
                {
                    var disciplina = await _disciplinaRepository.ObterDisciplinaPeloId(disciplinaId);

                    if (disciplina is null)
                        return NotFound($"Disciplina com id {disciplinaId} é inexistente.");

                    var disciplinaJaCursada = disciplinasAnteriores.Contains(disciplina);
                    var notaDaDisciplinaJaCursada = notasDoAluno.FirstOrDefault(not => not.DisciplinaId == disciplinaId);

                    if (disciplinaJaCursada && (bool)notaDaDisciplinaJaCursada.Aprovado)
                        return BadRequest($"O aluno já foi aprovado na disciplina: {disciplina.NomeDaDisciplina}");

                    var alunoDisciplina = new AlunoDisciplina()
                    {
                        AlunoId = alunoId,
                        DisciplinaId = disciplinaId
                    };

                    var notas = new Nota()
                    {
                        AlunoId = alunoId,
                        DisciplinaId = disciplinaId
                    };


                    disciplinasDesejadas.Add(alunoDisciplina);
                    gradeDeNotas.Add(notas);
                }
                await Task.WhenAll(
                 _alunoDisciplinaRepository.AdicionarAlunoNasDisciplinas(disciplinasDesejadas),
                 _notaRepository.CriarNotas(gradeDeNotas)
                );

                dbContextTransaction.Commit();
                return Ok("Rematrícula realizada com sucesso!");
            }
            catch (SqlException ex)
            {
                dbContextTransaction.Rollback();
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                dbContextTransaction.Rollback();
                return BadRequest($"Ocorreu problemas com uma das operações: {ex.Message}");
            }
            catch (Exception ex)
            {
                dbContextTransaction.Rollback();
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }


        [HttpPut("{alunoId}/atualizar")]
        public async Task<IActionResult> AtualizarAluno([FromRoute] int alunoId, [FromBody] Aluno alunoAtualizado)
        {
            if (!ModelState.IsValid)
                    return BadRequest("Insira os dados corretamente!");

            try
            {
                Aluno aluno = await _alunoRepository.ObterAlunoPeloId(alunoId);
                if(!AlunoValidator.ValidarAtualizacaoDoAluno(aluno, alunoAtualizado, out string errorMessage))
                {
                    return BadRequest(errorMessage);
                }
                await _alunoRepository.AtualizarAluno(aluno);
                AlunoDto alunoDto = _mapper.Map<AlunoDto>(aluno);
                return Ok("Informações alteradas com sucesso!");
            }
            catch (SqlException ex)
            {
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }


        [HttpDelete("{alunoId}/excluir")]
        public async Task<IActionResult> ExcluirAluno([FromRoute] int alunoId)
        {
            try
            {
                Aluno aluno = await _alunoRepository.ObterAlunoPeloId(alunoId);
                if (aluno is null)
                {
                    return NotFound("Aluno inexistente");
                }
                await _alunoRepository.ExcluirAluno(aluno);
                return Ok("Aluno excluído!");
            }
            catch (SqlException ex)
            {
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch (DbUpdateException ex)
            {
                return BadRequest($"Ocorreu problemas com uma das operações: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }
    }
}
