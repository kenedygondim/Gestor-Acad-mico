using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.SqlServer.Server;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoController(IAlunoRepository alunoRepository, IMapper mapper) : ControllerBase
    {
        private readonly IAlunoRepository _alunoRepository = alunoRepository;
        private readonly IMapper _mapper = mapper;

        readonly string[] status = ["Matrículado", "Trancado", "Formado", "Desistente", "Afastado"];
        readonly string[] generos = ["Masculino", "Feminino", "Outros"];

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AlunoDto>))]
        public async Task<IActionResult> GetAlunos()
        {
            try
            {
                var alunos = await _alunoRepository.GetAlunos();
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
        public async Task<IActionResult> GetAlunoPeloId(int alunoId)
        {
            try
            {
                var aluno = await _alunoRepository.GetAlunoPeloId(alunoId);

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
        public async Task<IActionResult> GetNotasDoAluno(int alunoId)
        {
            try
            {
                var notas = await _alunoRepository.GetNotasDoAluno(alunoId);

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
        public async Task<IActionResult> GetAlunoPeloNome(string nomeDoAluno)
        {
            try
            {
                var aluno = await _alunoRepository.GetAlunoPeloNome(nomeDoAluno);

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

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(AlunoDto))]
        public async Task<IActionResult> CriarAluno(Aluno aluno)
        {
            try
            {
                if (aluno == null || !ModelState.IsValid)
                    return BadRequest("Insira os dados corretamente");

                if (!status.Contains(aluno.StatusDoAluno, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira uma situação válida: 'Matrículado', 'Trancado', 'Formado', 'Desistente', 'Afastado'");

                if (aluno.Cpf.Length != 11)
                    return BadRequest("O campo CPF deve ter 11 dígitos sem pontos ou traços");

                await _alunoRepository.CriarAluno(aluno);

                var alunoDto = _mapper.Map<AlunoDto>(aluno);

                return Ok(alunoDto);
            }
            catch
            {
                return BadRequest("Não foi possível adicionar aluno");
            }
        }


        [HttpPut("{alunoId}/atualizar")]
        public async Task<IActionResult> AtualizarAluno([FromRoute] int alunoId, [FromBody] Aluno alunoAtualizado)
        {
            try
            {
                var aluno = await _alunoRepository.GetAlunoPeloId(alunoId);

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
