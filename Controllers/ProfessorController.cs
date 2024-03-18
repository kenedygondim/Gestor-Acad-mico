using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Repositories;
using Gestor_Acadêmico.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController(IProfessorRepository professorRepository, IMapper mapper) : ControllerBase
    { 
        private readonly IProfessorRepository _professorRepository = professorRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProfessorDto>))]
        public async Task<IActionResult> ObterProfessores()
        {
            try
            {
                IEnumerable<Professor> professores = await _professorRepository.ObterProfessores();
                if (professores is null)
                {
                    return NotFound("Nenhum professor encontrado");
                }
                IEnumerable<ProfessorDto> professorsDto = _mapper.Map<IEnumerable<ProfessorDto>>(professores);
                return Ok(professorsDto);
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

        [HttpGet("{professorId}/id")]
        [ProducesResponseType(200, Type = typeof(ProfessorDto))]
        public async Task<IActionResult> ObterProfessorPeloId([FromRoute] int professorId)
        {
            try
            {
                Professor professor = await _professorRepository.ObterProfessorPeloId(professorId);
                if (professor is null)
                {
                    return NotFound("Professor não encontrado");
                }
                ProfessorDto professorDto = _mapper.Map<ProfessorDto>(professor);
                return Ok(professorDto);
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

        /*[HttpGet("{nomeDoProfessor}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<ProfessorDto>))]
        public async Task<IActionResult> ObterProfessorPeloNome([FromRoute] string nomeDoProfessor)
        {
            try
            {
                IEnumerable<Professor> professor = await _professorRepository.ObterProfessorPeloNome(nomeDoProfessor);
                if (professor is null)
                {
                    return NotFound("Professor não encontrado");
                }
                IEnumerable<ProfessorDto> professorDto = _mapper.Map<IEnumerable<ProfessorDto>>(professor);
                return Ok(professorDto);
            }
            catch (SqlException ex)
            {
                return BadRequest($"Ocorreu um erro de servidor: {ex.Message}");
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro inesperado: {ex.Message}");
            }
        }*/

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ProfessorDto))]
        public async Task<IActionResult> CriarProfessor([FromBody] Professor professor)
        {
            if(!ModelState.IsValid)
                return BadRequest("Reveja os dados inseridos");

            var prontuariosEmUso = await _professorRepository.ObterProntuarios();


            if (!ProfessorValidation.ValidarCriacaoDoProfessor(professor, prontuariosEmUso,  out string errorMessage))
                return BadRequest(errorMessage);
     
            try
            {
                await _professorRepository.CriarProfessor(professor);
                ProfessorDto professorDto = _mapper.Map<ProfessorDto>(professor);
                return Ok(professorDto);
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

        [HttpPut("{professorId}/atualizar")]
        public async Task<IActionResult> AtualizarProfessor([FromRoute] int professorId, [FromBody] Professor professorAtualizado)
        {
            if (!ModelState.IsValid)
                return BadRequest("Reveja os dados inseridos");

            try
            {
                Professor professor = await _professorRepository.ObterProfessorPeloId(professorId);
                if(!ProfessorValidation.ValidarAtualizacaoDoProfessor(professor, professorAtualizado, out string errorMessage))
                {
                    return BadRequest(errorMessage);
                }
                await _professorRepository.AtualizarProfessor(professor);
                ProfessorDto professorDto = _mapper.Map<ProfessorDto>(professor);
                return Ok("Informações atualizadas com sucesso!");
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

        [HttpDelete("{professorId}/excluir")]
        public async Task<IActionResult> ExcluirProfessor([FromRoute] int professorId)
        {
            try
            {
                Professor professor = await _professorRepository.ObterProfessorPeloId(professorId);
                if (professor is null)
                {
                    return NotFound("Professor inexistente");
                }
                await _professorRepository.ExcluirProfessor(professor);
                return Ok("Professor excluído!");
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
