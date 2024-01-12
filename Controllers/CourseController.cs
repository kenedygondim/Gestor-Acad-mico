using AutoMapper;
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
    public class CourseController(ICourseRepository courseRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly IMapper _mapper = mapper;

        readonly string[] turnos = ["Matutino", "Vespertino", "Noturno", "Integral"];
        readonly string[] categorias = ["Tecnólogo", "Bacharelado", "Licenciatura", "Pós-graduação", "Cursos livres"];
        readonly string[] modalidades = ["Presencial", "EAD", "Híbrido"];

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CourseDto>))]
        public async Task<IActionResult> GetCourses()
        {
            try
            {
                var courses = await _courseRepository.GetCourses();
                var coursesDto = _mapper.Map<List<CourseDto>>(courses);
                return Ok(coursesDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar a lista de cursos");
            }
        }

        [HttpGet("{courseId}/id")]
        [ProducesResponseType(200, Type = typeof(CourseDto))]
        public async Task<IActionResult> GetCourseById(int courseId)
        {
            try
            {
                var course = await _courseRepository.GetCourseById(courseId);

                if(course == null)
                   return NotFound("Curso não encontrado");


                var courseDto = _mapper.Map<CourseDto>(course);
                return Ok(courseDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar o curso solicitado");
            }
        }

        [HttpGet("{courseName}")]
        [ProducesResponseType(200, Type = typeof(CourseDto))]
        public async Task<IActionResult> GetCourseByName(string courseName)
        {
            try
            {
                var course = await _courseRepository.GetCourseByName(courseName);

                if (course == null)
                    return NotFound("Curso não encontrado");

                var courseDto = _mapper.Map<CourseDto>(course);
                return Ok(courseDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar o curso solicitado");
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(CourseDto))]
        public async Task<IActionResult> CreateCourse (Course course)
        {
            try
            {
                if(course == null || !ModelState.IsValid)
                    return BadRequest ("Insira os dados corretamente");

                if(!turnos.Contains(course.Turn, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira um turno válido: 'Matutino', 'Vespertino', 'Nortuno' ou 'Integral'");

                if (!categorias.Contains(course.CategoryCourse, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira uma categoria válida: 'Tecnólogo', 'Bacharelado', 'Licenciatura', 'Pós-graduação' ou 'Cursos livres'");

                if (!modalidades.Contains(course.Mode, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira uma modalidade válida: 'Presencial', 'EAD', 'Híbrido'");

                await _courseRepository.CreateCourse(course);

                var courseDto = _mapper.Map<CourseDto>(course);

                return Ok(courseDto);
            }
            catch 
            {
                return BadRequest("Não foi possível criar o curso solicitado");
            }
        }

        

        [HttpPut("{courseId}/update")]
        public async Task<IActionResult> UpdateCourse([FromRoute] int courseId, [FromBody] Course courseUpdated)
        { 
            try
            {
                var course = await _courseRepository.GetCourseById(courseId);

                if (course == null)
                    return NotFound("Curso inexistente");

                if (!ModelState.IsValid) 
                    return BadRequest("Reveja os dados inseridos");

                if (course.Id != courseUpdated.Id)
                    return BadRequest("Ocorreu um erro na validação dos identificadores.");

                if(!turnos.Contains(courseUpdated.Turn, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira um turno válido: 'Matutino', 'Vespertino', 'Nortuno' ou 'Integral'");

                if (!categorias.Contains(courseUpdated.CategoryCourse, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira uma categoria válida: 'Tecnólogo', 'Bacharelado', 'Licenciatura', 'Pós-graduação' ou 'Cursos livres'");

                if (!modalidades.Contains(courseUpdated.Mode, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira uma modalidade válida: 'Presencial', 'EAD', 'Híbrido'");

                await _courseRepository.UpdateCourse(course);

                var courseDto = _mapper.Map<CourseDto>(course);

                return Ok("Curso alterado com sucesso!");
            }

            catch
            {
                return BadRequest("Não foi possível editar a categoria");
            }
        }


        [HttpDelete("{courseId}/delete")]
        public async Task<IActionResult> DeleteCourse([FromRoute] int courseId)
        {
            try
            {
                var course = await _courseRepository.GetCourseById(courseId);

                if (course == null)
                    return NotFound("Curso inexistente");

                await _courseRepository.DeleteCourse(course);
                return Ok("Curso excluído!");
            }
            catch
            {
                return BadRequest("Não foi possível excluir a categoria");
            }
        }


    }
}