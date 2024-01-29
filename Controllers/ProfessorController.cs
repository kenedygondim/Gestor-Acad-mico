using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfessorController(IProfessorRepository professorRepository, IMapper mapper) : ControllerBase
    { 

        private readonly IProfessorRepository _professorRepository = professorRepository;
        private readonly IMapper _mapper = mapper;

        //Array de gêneros para validação
        readonly string[] generos = ["Masculino", "Feminino", "Outros"];

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
        [ProducesResponseType(200, Type = typeof(ProfessorDto))]
        public async Task<IActionResult> ObterProfessorPeloNome([FromRoute] string nomeDoProfessor)
        {
            try
            {
                var professor = await _professorRepository.ObterProfessorPeloNome(nomeDoProfessor);

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

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(ProfessorDto))]
        public async Task<IActionResult> CriarProfessor([FromBody] Professor professor)
        {
            try
            {
                if (professor == null || !ModelState.IsValid)
                    return BadRequest("Insira os dados corretamente");

                if (!generos.Contains(professor.Genero, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira um gênero válido: 'Masculino', 'Feminino, 'Outros'");

                if (professor.Cpf.Length != 11)
                    return BadRequest("O campo CPF deve ter 11 dígitos sem pontos ou traços");

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
            try
            {
                var professor = await _professorRepository.ObterProfessorPeloId(professorId);

                if (professor == null)
                    return NotFound("Professor inexistente");

                if (!ModelState.IsValid)
                    return BadRequest("Reveja os dados inseridos");

                if (professor.Id != professorAtualizado.Id)
                    return BadRequest("Ocorreu um erro na validação dos identificadores.");

                await _professorRepository.AtualizarProfessor(professor);

                return Ok("Informações atualizadas com sucesso!");
            }

            catch
            {
                return BadRequest("Não foi possível editar o professor");
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
