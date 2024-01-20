using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

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
        public async Task<IActionResult> GetDisciplinas()
        {
            try
            {
                var disciplinas = await _disciplinaRepository.GetDisciplinas();
                var disciplinasDto = _mapper.Map<List<DisciplinaDto>>(disciplinas);
                return Ok(disciplinasDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar a lista de disciplinas");
            }
        }

        [HttpGet("{cursoId}/curso/disciplinas")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DisciplinaDto>))]
        public async Task<IActionResult> GetDisciplinasDoCurso([FromRoute] int cursoId)
        {
            try
            {
                var disciplinas = await _disciplinaRepository.GetDisciplinasDoCurso(cursoId);
                var disciplinasDto = _mapper.Map<List<DisciplinaDto>>(disciplinas);
                return Ok(disciplinasDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar a lista de disciplinas do curso");
            }
        }

        [HttpGet("{professorId}/professor/disciplinas")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<DisciplinaDto>))]
        public async Task<IActionResult> GetDisciplinasDoProfessor([FromRoute] int professorId)
        {
            try
            {
                var disciplinas = await _disciplinaRepository.GetDisciplinasDoProfessor(professorId);
                var disciplinasDto = _mapper.Map<List<DisciplinaDto>>(disciplinas);
                return Ok(disciplinasDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar a lista de disciplinas do professor");
            }
        }

        [HttpGet("{disciplinaId}/id")]
        [ProducesResponseType(200, Type = typeof(DisciplinaDto))]
        public async Task<IActionResult> GetDisciplinaPeloId(int disciplinaId)
        {
            try
            {
                var disciplina = await _disciplinaRepository.GetDisciplinaPeloId(disciplinaId);

                if (disciplina == null)
                    return NotFound("Disciplina não encontrada");


                var disciplinaDto = _mapper.Map<DisciplinaDto>(disciplina);
                return Ok(disciplinaDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar a disciplina solicitada");
            }
        }

        [HttpGet("{nomeDaDisciplina}")]
        [ProducesResponseType(200, Type = typeof(DisciplinaDto))]
        public async Task<IActionResult> GetDisciplinaByName(string nomeDaDisciplina)
        {
            try
            {
                var disciplina = await _disciplinaRepository.GetDisciplinaPeloNome(nomeDaDisciplina);

                if (disciplina == null)
                    return NotFound("Disciplina não encontrada");

                var disciplinaDto = _mapper.Map<DisciplinaDto>(disciplina);
                return Ok(disciplinaDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar a disciplina solicitado");
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(DisciplinaDto))]
        public async Task<IActionResult> CriarDisciplina(Disciplina disciplina)
        {
            try
            {
                var disciplinasDoCurso = await _disciplinaRepository.GetDisciplinasDoCurso(disciplina.CursoId);
                var disciplinasDoSemestreDeReferencia = disciplinasDoCurso.Where(dis => dis.SemestreDeReferencia == disciplina.SemestreDeReferencia);
                var contagem = disciplinasDoSemestreDeReferencia.Sum(a => a.AulasPorSemana) + disciplina.AulasPorSemana; 

                if (disciplina == null || !ModelState.IsValid)
                    return BadRequest("Insira os dados corretamente");

                if(contagem > 25)
                    return BadRequest($"Número de aulas por semana ({contagem}) maior que o limite (25). Escolha uma disciplina que se encaixe");

                await _disciplinaRepository.CriarDisciplina(disciplina);

                var disciplinaDto = _mapper.Map<DisciplinaDto>(disciplina);

                return Ok(disciplinaDto);
            }
            catch
            {
                return BadRequest("Não foi possível criar a disciplina");
            }
        } 
    }

}
