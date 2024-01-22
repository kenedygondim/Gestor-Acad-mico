using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunoCursoController(IAlunoCursoRepository alunoCursoRepository, IAlunoRepository alunoRepository,ICursoRepository cursoRepository ,IMapper mapper) : ControllerBase
    {
        private readonly IAlunoCursoRepository _alunoCursoRepository = alunoCursoRepository;
        private readonly IAlunoRepository _alunoRepository = alunoRepository;
        private readonly ICursoRepository _cursoRepository = cursoRepository;

        [HttpPost("/{cursoId}")]
        [ProducesResponseType(200, Type = typeof(IEnumerable<AlunoDto>))]
        public async Task<IActionResult> AdicionarVariosAlunosAoCurso([FromBody] int[] alunosIds, [FromRoute] int cursoId)
        {
            try
            {
                var curso = await _cursoRepository.GetCursoPeloId(cursoId);

                if (curso == null)
                {
                     return NotFound("Curso não encontrado");
                }

                foreach (var alunoId in alunosIds)
                {
                    var aluno = await _alunoRepository.GetAlunoPeloId(alunoId);

                    if (aluno == null)
                        return NotFound($"Aluno: {alunoId} não encontrado");

                    var verificaSeAlunoJaEstaNoCurso = await _alunoCursoRepository.ObterAlunoCursoPeloId(alunoId, cursoId);

                    if (verificaSeAlunoJaEstaNoCurso == null)
                        return BadRequest($"Aluno: {alunoId} já está no curso");

                    var alunoCurso = new AlunoCurso
                    {
                        AlunoId = alunoId,
                        CursoId = cursoId
                    };

                    await _alunoCursoRepository.AdicionarVariosAlunosAoCurso(alunoCurso);
                }   

                return Ok("Alunos adicionados.");
            }
            catch (Exception e)
            {
                return BadRequest("Não foi possível adicionar alunos ao curso: " + e.Message);
            }
        }

    }
}
