using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Repositories;
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

        static readonly string[] generos = ["Masculino", "Feminino", "Não-binário", "Gênero fluido", "Agênero", "Bigênero", "Travesti", "Cisgênero", "Transgênero"];
        static readonly string patternCpf = @"(\d{3}\.){2}\d{3}-\d{2}";
        static readonly string patternEmail = @"([a-z0-9\.\-_]{2,})@([a-z0-9]{2,})(\.[a-z]{2,})?(\.[a-z]{2,})?(\.[a-z]{2,})";
        static readonly string patternNumeroDeTelefone = @"(\d{2})\s(\d{4,5}-\d{4})";

        private static string ObterNomeCompleto(string primeiroNome, string sobrenome) => $"{primeiroNome} {sobrenome}";
        private static bool ValidarCpf(string cpf) => Regex.IsMatch(cpf, patternCpf);
        private static bool ValidarEmail(string email) => Regex.IsMatch(email, patternEmail);
        private static bool ValidarNumeroDeTelefone(string? numeroDeTelefone) => Regex.IsMatch(numeroDeTelefone, patternNumeroDeTelefone);
        private static bool ValidarGenero(string genero) => generos.Contains(genero);


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
            if (professor == null || !ModelState.IsValid)
                return BadRequest("Insira os dados corretamente!");

            if (!ValidarGenero(professor.Genero))
                return BadRequest($"Insira um genêro válido: {string.Join(", ", generos)}.");

            if (!ValidarCpf(professor.Cpf))
                return BadRequest("CPF inválido! Padrão desejado: XXX.XXX.XXX-XX");

            if (!ValidarEmail(professor.EnderecoDeEmail))
                return BadRequest("Endereço de e-mail inválido!");

            if (!ValidarNumeroDeTelefone(professor.NumeroDeTelefone))
                return BadRequest("Número de telefone inválido! Padrão desejado: XX XXXXX-XXXX");

            try
            {
                professor.NomeCompleto = ObterNomeCompleto(professor.PrimeiroNome, professor.Sobrenome);

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
                    return NotFound("Aluno inexistente");

                if (!ModelState.IsValid)
                    return BadRequest("Reveja os dados inseridos");

                if (professor.Id != professorAtualizado.Id)
                    return BadRequest("Ocorreu um erro na validação dos identificadores.");

                if(professorAtualizado.Cpf != professor.Cpf)
                    return BadRequest("Não é possível alterar o CPF do professor.");

                if (professorAtualizado.PrimeiroNome == "" || professorAtualizado.Sobrenome == "")
                    return BadRequest("Preencha os campos de primeiro nome e sobrenome!");

                if (professorAtualizado.DataDeNascimento != professor.DataDeNascimento)
                    return BadRequest("Não é possível alterar a data de nascimento!");

                if (!ValidarGenero(professor.Genero))
                    return BadRequest($"Insira um genêro válido: {string.Join(", ", generos)}.");

                professor.PrimeiroNome = professorAtualizado.PrimeiroNome;
                professor.Sobrenome = professorAtualizado.Sobrenome;
                professor.NomeCompleto = ObterNomeCompleto(professorAtualizado.PrimeiroNome, professorAtualizado.Sobrenome);
                professor.EnderecoDeEmail = professorAtualizado.EnderecoDeEmail;
                professor.NumeroDeTelefone = professorAtualizado.NumeroDeTelefone;
                professor.Endereco = professorAtualizado.Endereco;
                professor.Genero = professorAtualizado.Genero;

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
