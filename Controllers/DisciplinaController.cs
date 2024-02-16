using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisciplinaController(IDisciplinaRepository disciplinaRepository ,IMapper mapper) : ControllerBase
    {
        private readonly IDisciplinaRepository _disciplinaRepository = disciplinaRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DisciplinaDto>))]
        public async Task<IActionResult> ObterDisciplinas()
        {
            try
            {
                IEnumerable<Disciplina> disciplinas = await _disciplinaRepository.ObterDisciplinas();
                if (disciplinas is null)
                {
                    return NotFound("Nenhuma disciplina encontrada.");
                }
                IEnumerable<DisciplinaDto> disciplinasDto = _mapper.Map<IEnumerable<DisciplinaDto>>(disciplinas);
                return Ok(disciplinasDto);
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

        [HttpGet("{cursoId}/curso/disciplinas")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DisciplinaDto>))]
        public async Task<IActionResult> ObterDisciplinasDoCurso([FromRoute] int cursoId)
        {
            try
            {
                IEnumerable<Disciplina> disciplinas = await _disciplinaRepository.ObterDisciplinasDoCurso(cursoId);
                if (disciplinas is null)
                {
                    return NotFound("Nenhuma disciplina encontrada.");
                }
                IEnumerable<DisciplinaDto> disciplinasDto = _mapper.Map<IEnumerable<DisciplinaDto>>(disciplinas);
                return Ok(disciplinasDto);
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

        [HttpGet("{professorId}/professor/disciplinas")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DisciplinaDto>))]
        public async Task<IActionResult> ObterDisciplinasDoProfessor([FromRoute] int professorId)
        {
            try
            {
                IEnumerable<Disciplina> disciplinas = await _disciplinaRepository.ObterDisciplinasDoProfessor(professorId);
                if (disciplinas is null)
                {
                    return NotFound("Nenhuma disciplina encontrada.");
                }
                IEnumerable<DisciplinaDto> disciplinasDto = _mapper.Map<IEnumerable<DisciplinaDto>>(disciplinas);
                return Ok(disciplinasDto);
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

        [HttpGet("{disciplinaId}/id")]
        [ProducesResponseType(200, Type = typeof(DisciplinaDto))]
        public async Task<IActionResult> ObterDisciplinaPeloId([FromRoute] int disciplinaId)
        {
            try
            {
                Disciplina disciplina = await _disciplinaRepository.ObterDisciplinaPeloId(disciplinaId);
                if (disciplina is null)
                {
                    return NotFound("Disciplina não encontrada");
                }
                DisciplinaDto disciplinaDto = _mapper.Map<DisciplinaDto>(disciplina);
                return Ok(disciplinaDto);
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

        [HttpGet("{nomeDaDisciplina}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DisciplinaDto>))]
        public async Task<IActionResult> ObterDisciplinaByName([FromRoute] string nomeDaDisciplina)
        {
            try
            {
                IEnumerable<Disciplina> disciplina = await _disciplinaRepository.ObterDisciplinaPeloNome(nomeDaDisciplina);
                if (disciplina is null)
                {
                    return NotFound("Disciplina não encontrada");
                }
                IEnumerable<DisciplinaDto> disciplinaDto = _mapper.Map<IEnumerable<DisciplinaDto>>(disciplina);
                return Ok(disciplinaDto);
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
        [ProducesResponseType(200, Type = typeof(DisciplinaDto))]
        public async Task<IActionResult> CriarDisciplina([FromBody] Disciplina disciplina)
        {
            if (!ModelState.IsValid)
                return BadRequest("Insira os dados corretamente");

            if (disciplina == null)
                return BadRequest("Insira os dados corretamente");

            try
            {
                IEnumerable<Disciplina> disciplinasDoCurso = await _disciplinaRepository.ObterDisciplinasDoCurso(disciplina.CursoId);
                IEnumerable<Disciplina> disciplinasDoSemestreDeReferencia = disciplinasDoCurso.Where(dis => dis.SemestreDeReferencia == disciplina.SemestreDeReferencia);
                int contagem = disciplinasDoSemestreDeReferencia.Sum(a => a.AulasPorSemana) + disciplina.AulasPorSemana; 
                if(contagem > 25)
                {
                    return BadRequest($"Número de aulas por semana ({contagem}) maior que o limite (25). Escolha uma disciplina que se encaixe");

                }
                await _disciplinaRepository.CriarDisciplina(disciplina);
                DisciplinaDto disciplinaDto = _mapper.Map<DisciplinaDto>(disciplina);
                return Ok(disciplinaDto);
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
