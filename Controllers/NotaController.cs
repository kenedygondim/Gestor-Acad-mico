using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController
        (
        INotaRepository NotaRepository,
        IAlunoRepository studentRepository,
        IDisciplinaRepository subjectRepository,
        IMapper mapper
        ) 
        : ControllerBase
    {
        private readonly INotaRepository _NotaRepository = NotaRepository;
        private readonly IDisciplinaRepository _subjectRepository = subjectRepository;
        private readonly IMapper _mapper = mapper;

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(NotaDto))]
        public async Task<IActionResult> CriarNota(Nota nota)
        {
            try
            {
                if (nota == null || !ModelState.IsValid)
                    return BadRequest("Insira os dados corretamente");


                var subject = await _subjectRepository.ObterDisciplinaPeloId(nota.DisciplinaId);

                if (subject == null)
                    return BadRequest("Essa disciplina não existe");

                await _NotaRepository.CriarNota(nota);

                var notaDto = _mapper.Map<NotaDto>(nota);

                return Ok(notaDto);
            }
            catch
            {
                return BadRequest("Não foi possível criar nota");
            }
        }
    }
}
