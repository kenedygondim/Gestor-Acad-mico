using AutoMapper;
using Gestor_Acadêmico.Context;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Validation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotaController
        (
        INotaRepository NotaRepository,
        IAlunoRepository alunoRepository,
        IDisciplinaRepository disciplinaRepository,
        GestorAcademicoContext context,
        IMapper mapper
        ) 
        : ControllerBase
    {
        private readonly INotaRepository _NotaRepository = NotaRepository;
        private readonly IAlunoRepository _alunoRepository = alunoRepository;
        private readonly GestorAcademicoContext _context = context;
        private readonly IMapper _mapper = mapper;

        [HttpPut("{notaId}/atualizar")]
        [ProducesResponseType(200, Type = typeof(NotaDto))]
        public async Task<IActionResult> AtualizarNota([FromRoute] int notaId, [FromBody] Nota notasRecebidas)
        {
            if (!ModelState.IsValid)
                return BadRequest("Insira os dados corretamente!");
                using var dbContextTransaction = _context.Database.BeginTransaction();

            try
            {
                Nota nota = await _NotaRepository.ObterNotaEspecifica(notasRecebidas.Id);

                if (!NotaValidation.ValidarAtualizacaoDaNota(nota, notasRecebidas, out string errorMessage))
                {
                    return BadRequest(errorMessage);
                }

                await _NotaRepository.AtualizarNota(nota);

                Aluno aluno = await _alunoRepository.ObterAlunoPeloId(nota.AlunoId);


                if (aluno.Notas.Any())
                {
                    var teste = aluno.Notas.Where(not => not.NotasFechadas).ToList();
                    aluno.IRA = teste.Sum(not => not.MediaGeral) / teste.Count();
                }

               await _alunoRepository.AtualizarAluno(aluno);
               NotaDto notaDto = _mapper.Map<NotaDto>(nota);
                dbContextTransaction.Commit();

                return Ok(notaDto);
            }
            catch (Exception e)
            {
                dbContextTransaction.Rollback();
                return BadRequest("Não foi possível atualizar nota" + e.Message);
            }
        }
    }
}
