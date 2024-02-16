using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Repositories;
using Gestor_Acadêmico.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
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
                var professores = await _professorRepository.ObterProfessores();
                var professorsDto = _mapper.Map<List<ProfessorDto>>(professores);
                return Ok(professorsDto);
            }
            catch
            {
                return BadRequest($"Não foi possível recuperar a lista de professores.");
            }
        }

        [HttpGet("{professorId}/id")]
        [ProducesResponseType(200, Type = typeof(ProfessorDto))]
        public async Task<IActionResult> ObterProfessorPeloId([FromRoute] int professorId)
        {
            try
            {
                var professor = await _professorRepository.ObterProfessorPeloId(professorId);

                if (professor == null)
                    return NotFound("Professor não encontrado");


                var professorDto = _mapper.Map<ProfessorDto>(professor);
                return Ok(professorDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar o professor solicitado");
            }
        }

        [HttpGet("{nomeDoProfessor}")]
        [ProducesResponseType(200, Type = typeof(List<ProfessorDto>))]
        public async Task<IActionResult> ObterProfessorPeloNome([FromRoute] string nomeDoProfessor)
        {
            try
            {
                var professor = await _professorRepository.ObterProfessorPeloNome(nomeDoProfessor);

                if (professor == null)
                    return NotFound("Professor não encontrado");

                var professorDto = _mapper.Map<List<ProfessorDto>>(professor);
                return Ok(professorDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar o professor solicitado");
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ProfessorDto))]
        public async Task<IActionResult> CriarProfessor([FromBody] Professor professor)
        {
            if(!ModelState.IsValid)
                return BadRequest("Reveja os dados inseridos");

            if (!ProfessorValidation.ValidarCriacaoDoProfessor(professor, out string errorMessage))
                return BadRequest(errorMessage);
            
            try
            {
                await _professorRepository.CriarProfessor(professor);

                var professorDto = _mapper.Map<ProfessorDto>(professor);

                return Ok(professorDto);
            }
            catch
            {
                return BadRequest("Não foi possível adicionar o professor");
            }
        }

        [HttpPut("{professorId}/atualizar")]
        public async Task<IActionResult> AtualizarProfessor([FromRoute] int professorId, [FromBody] Professor professorAtualizado)
        {
            if (!ModelState.IsValid)
                return BadRequest("Reveja os dados inseridos");

            try
            {
                var professor = await _professorRepository.ObterProfessorPeloId(professorId);

                if(!ProfessorValidation.ValidarAtualizacaoDoProfessor(professor, professorAtualizado, out string errorMessage)) 
                    return BadRequest(errorMessage);

                await _professorRepository.AtualizarProfessor(professor);

                var professorDto = _mapper.Map<ProfessorDto>(professor);

                return Ok("Informações alteradas com sucesso!");
            }

            catch (Exception e)
            {
                return BadRequest($"Não foi possível atualizar as informações do professor {e.Message}");
            }
        }

        [HttpDelete("{professorId}/excluir")]
        public async Task<IActionResult> ExcluirProfessor([FromRoute] int professorId)
        {
            try
            {
                var professor = await _professorRepository.ObterProfessorPeloId(professorId);

                if (professor == null)
                    return NotFound("Professor inexistente");

                await _professorRepository.ExcluirProfessor(professor);
                return Ok("Professor excluído!");
            }
            catch
            {
                return BadRequest("Não foi possível excluir  o professor");
            }
        }

       
    }
}
