using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController(ICursoRepository cursoRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository = cursoRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CursoDto>))]
        public async Task<IActionResult> ObterCursos()
        {
            try
            {
                IEnumerable<Curso> cursos = await _cursoRepository.ObterCursos();
                if (cursos is null)
                {
                    return NotFound("Nenhum curso encontrado");
                }
                IEnumerable<CursoDto> cursosDto = _mapper.Map<List<CursoDto>>(cursos);
                return Ok(cursosDto);
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

        [HttpGet("{cursoId}/id")]
        [ProducesResponseType(200, Type = typeof(CursoDto))]
        public async Task<IActionResult> ObterCursoPeloId([FromRoute] int cursoId)
        {
            try
            {
                Curso curso = await _cursoRepository.ObterCursoPeloId(cursoId);
                if(curso is null)
                {
                    return NotFound("Curso não encontrado.");
                }
                CursoDto cursoDto = _mapper.Map<CursoDto>(curso);
                return Ok(cursoDto);
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

        [HttpGet("{nomeDoCurso}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CursoDto>))]
        public async Task<IActionResult> ObterCursoPeloNome([FromRoute] string nomeDoCurso)
        {
            try
            {
                IEnumerable<Curso> curso = await _cursoRepository.ObterCursoPeloNome(nomeDoCurso);
                if (curso is null)
                {
                    return NotFound("Curso não encontrado");
                }
                IEnumerable<CursoDto> cursoDto = _mapper.Map<IEnumerable<CursoDto>>(curso);
                return Ok(cursoDto);
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
        [ProducesResponseType(200, Type = typeof(CursoDto))]
        public async Task<IActionResult> CriarCurso ([FromBody] Curso curso)
        {
            if (!ModelState.IsValid)
                return BadRequest("Insira os dados corretamente");

            try
            {
                if (!CursoValidation.ValidarCriacaoDoCurso(curso, out string errorMessage))
                {
                    return BadRequest(errorMessage);
                }
                await _cursoRepository.CriarCurso(curso);
                CursoDto cursoDto = _mapper.Map<CursoDto>(curso);
                return Ok(cursoDto);
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

        [HttpPut("{cursoId}/atualizar")]
        public async Task<IActionResult> AtualizarCurso([FromRoute] int cursoId, [FromBody] Curso cursoAtualizado)
        {
            if (!ModelState.IsValid)
                return BadRequest("Reveja os dados inseridos");

            try
            {
                Curso curso = await _cursoRepository.ObterCursoPeloId(cursoId);
                if (!CursoValidation.ValidarAtualizacaoDoCurso(curso, cursoAtualizado, out string errorMessage))
                {
                    return BadRequest(errorMessage);
                }
                await _cursoRepository.AtualizarCurso(curso);
                CursoDto cursoDto = _mapper.Map<CursoDto>(curso);
                return Ok("Curso atualizado com sucesso!");
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