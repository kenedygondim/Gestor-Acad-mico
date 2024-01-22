﻿using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Identity.Client;
using System.Linq;
using System.Text.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursoController(ICursoRepository cursoRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICursoRepository _cursoRepository = cursoRepository;
        private readonly IMapper _mapper = mapper;

        readonly string[] turnos = ["Matutino", "Vespertino", "Noturno", "Integral"];
        readonly string[] categorias = ["Tecnólogo", "Bacharelado", "Licenciatura", "Pós-graduação", "Cursos livres"];
        readonly string[] modalidades = ["Presencial", "EAD", "Híbrido"];

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CursoDto>))]
        public async Task<IActionResult> GetCursos()
        {
            try
            {
                var cursos = await _cursoRepository.GetCursos();
                var cursosDto = _mapper.Map<List<CursoDto>>(cursos);
                return Ok(cursosDto);
            }
            catch (Exception ex) 
            {
                return BadRequest($"Não foi possível recuperar a lista de cursos. Excessão: {ex.Message}");
            }
        }

        [HttpGet("{cursoId}/id")]
        [ProducesResponseType(200, Type = typeof(CursoDto))]
        public async Task<IActionResult> GetCursoPeloId(int cursoId)
        {
            try
            {
                var curso = await _cursoRepository.GetCursoPeloId(cursoId);

                if(curso == null)
                   return NotFound("Curso não encontrado");


                var cursoDto = _mapper.Map<CursoDto>(curso);
                return Ok(cursoDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar o curso solicitado");
            }
        }

        [HttpGet("{nomeDoCurso}")]
        [ProducesResponseType(200, Type = typeof(CursoDto))]
        public async Task<IActionResult> GetCursoPeloNome(string nomeDoCurso)
        {
            try
            {
                var curso = await _cursoRepository.GetCursoPeloNome(nomeDoCurso);

                if (curso == null)
                    return NotFound("Curso não encontrado");

                var cursoDto = _mapper.Map<CursoDto>(curso);
                return Ok(cursoDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar o curso solicitado");
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CursoDto))]
        public async Task<IActionResult> CriarCurso (Curso curso)
        {
            try
            {
                if(curso == null || !ModelState.IsValid)
                    return BadRequest ("Insira os dados corretamente");

                if(!turnos.Contains(curso.Turno, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira um turno válido: 'Matutino', 'Vespertino', 'Nortuno' ou 'Integral'");

                if (!categorias.Contains(curso.CategoriaDoCurso, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira uma categoria válida: 'Tecnólogo', 'Bacharelado', 'Licenciatura', 'Pós-graduação' ou 'Cursos livres'");

                if (!modalidades.Contains(curso.Modalidade, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira uma modalidade válida: 'Presencial', 'EAD', 'Híbrido'");

                await _cursoRepository.CriarCurso(curso);

                var cursoDto = _mapper.Map<CursoDto>(curso);

                return Ok(cursoDto);
            }
            catch 
            {
                return BadRequest("Não foi possível criar o curso.");
            }
        }

        

        [HttpPut("{cursoId}/atualizar")]
        public async Task<IActionResult> AtualizarCurso([FromRoute] int cursoId, [FromBody] Curso cursoAtualizado)
        { 
            try
            {
                var curso = await _cursoRepository.GetCursoPeloId(cursoId);

                if (curso == null)
                    return NotFound("Curso inexistente");

                if (!ModelState.IsValid) 
                    return BadRequest("Reveja os dados inseridos");

                if (curso.Id != cursoAtualizado.Id)
                    return BadRequest("Ocorreu um erro na validação dos identificadores.");

                if(!turnos.Contains(cursoAtualizado.Turno, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira um turno válido: 'Matutino', 'Vespertino', 'Nortuno' ou 'Integral'");

                if (!categorias.Contains(cursoAtualizado.CategoriaDoCurso, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira uma categoria válida: 'Tecnólogo', 'Bacharelado', 'Licenciatura', 'Pós-graduação' ou 'Cursos livres'");

                if (!modalidades.Contains(cursoAtualizado.Modalidade, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira uma modalidade válida: 'Presencial', 'EAD', 'Híbrido'");

                await _cursoRepository.AtualizarCurso(curso);

                var cursoDto = _mapper.Map<CursoDto>(curso);

                return Ok("Curso alterado com sucesso!");
            }

            catch
            {
                return BadRequest("Não foi possível atualizar o curso");
            }
        }
    }
}