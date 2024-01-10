using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController(ICourseRepository courseRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICourseRepository _courseRepository = courseRepository;
        private readonly IMapper _mapper = mapper;


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

    }
}