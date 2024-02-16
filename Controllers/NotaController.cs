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
        IAlunoRepository alunoRepository,
        IDisciplinaRepository disciplinaRepository,
        IMapper mapper
        ) 
        : ControllerBase
    {
        private readonly INotaRepository _NotaRepository = NotaRepository;
        private readonly IAlunoRepository _alunoRepository = alunoRepository;
        private readonly IMapper _mapper = mapper;

        [HttpPut("{notaId}/atualizar")]
        [ProducesResponseType(200, Type = typeof(NotaDto))]
        public async Task<IActionResult> AtualizarNota([FromRoute] int notaId, [FromBody] Nota notasRecebidas)
        {
            try
            {
                if (notaId != notasRecebidas.Id)
                    return BadRequest("Id da nota não encontrado");

                if (notasRecebidas == null || !ModelState.IsValid )
                    return BadRequest("Insira os dados corretamente");

                var nota = await _NotaRepository.ObterNotaEspecifica(notasRecebidas.Id);

                if (nota == null)
                    return BadRequest("Notas não encontradas");

                nota.FrequenciaDoAluno = notasRecebidas.FrequenciaDoAluno;
                nota.PrimeiraAvaliacao = notasRecebidas.PrimeiraAvaliacao;
                nota.SegundaAvaliacao = notasRecebidas.SegundaAvaliacao;
                nota.Atividades = notasRecebidas.Atividades;
                nota.MediaGeral = (decimal)((nota.PrimeiraAvaliacao + nota.SegundaAvaliacao + nota.Atividades) / 3);
                nota.NotasFechadas = notasRecebidas.NotasFechadas;

                if(nota.NotasFechadas && nota.MediaGeral > 6 && nota.FrequenciaDoAluno > 75)
                {
                    nota.Aprovado = true;
                } 
                else
                {
                    nota.Aprovado = false;
                }    

                await _NotaRepository.AtualizarNota(nota);

                try
                {
                    var aluno = await _alunoRepository.ObterAlunoPeloId((int)nota.AlunoId);

                    if (aluno.Notas.Any())
                    {
                        var teste = aluno.Notas.Where(not => not.NotasFechadas).ToList();
                        aluno.IRA = teste.Sum(not => not.MediaGeral) / teste.Count();
                    }

                    await _alunoRepository.AtualizarAluno(aluno);

                }
                catch
                {
                    return BadRequest("Não foi possível atualizar o IRA do aluno");
                }


                var notaDto = _mapper.Map<NotaDto>(nota);

                return Ok(notaDto);
            }
            catch (Exception e)
            {
                return BadRequest("Não foi possível atualizar nota" + e.Message);
            }
        }
        ///////////////////////////////////////////////////////////////////////
    }
}
