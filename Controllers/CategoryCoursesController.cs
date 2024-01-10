using AutoMapper;
using Gestor_Acadêmico.Dto;
using Gestor_Acadêmico.Interfaces;
using Gestor_Acadêmico.Models;
using Gestor_Acadêmico.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryCoursesController(ICategoryCourseRepository categoryCourseRepository, IMapper mapper) : ControllerBase
    {
        private readonly ICategoryCourseRepository _categoryCourseRepository = categoryCourseRepository;
        private readonly IMapper _mapper = mapper;

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<CategoryCourseDto>))]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var categories = await _categoryCourseRepository.GetCategoriesCourse();
                var categoriesDto = _mapper.Map<List<CategoryCourseDto>>(categories);
                return Ok(categoriesDto);
            }
            catch 
            {
                return BadRequest("Não foi possível recuperar a lista de categorias de curso");
            }
        }

        [HttpGet("{categoryId}/id")]
        [ProducesResponseType(200, Type = typeof(CategoryCourseDto))]
        public async Task<IActionResult> GetCategorieCourseById([FromRoute] int categoryId)
        {
            try
            {
                var category = await _categoryCourseRepository.GetCategoryCourseById(categoryId);

                if(category == null)
                    return NotFound("Categoria de curso não encontrada");

                var categoryDto = _mapper.Map<CategoryCourseDto>(category);

                return Ok(categoryDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar a categoria de curso solicitada");
            }
        }

        [HttpGet("{categoryName}")]
        [ProducesResponseType(200, Type = typeof(CategoryCourseDto))]
        public async Task<IActionResult> GetCategorieCourseByName([FromRoute] string categoryName)
        {
            try
            {
                string stringFormatted = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(categoryName);

                var category = await _categoryCourseRepository.GetCategoryCourseByName(categoryName);

                Console.Write(stringFormatted);

                if (category == null)
                    return NotFound("Categoria de curso não encontrada");

                var categoryDto = _mapper.Map<CategoryCourseDto>(category);

                return Ok(categoryDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar a categoria de curso solicitada");
            }
        }

        [HttpPost]
        [ProducesResponseType(201, Type = typeof(CategoryCourseDto))]
        public async Task<IActionResult> CreateCategory ([FromBody] CategoryCourseDto categoryCreate)
        {
            try
            {
                if (categoryCreate == null)
                    return BadRequest("Insira os dados de criação");

                if (!ModelState.IsValid)
                    return BadRequest("Reveja os dados inseridos");

                await _categoryCourseRepository.CreateCategory(categoryCreate);

                return CreatedAtAction(nameof(GetCategorieCourseById), new { categoryId = categoryCreate.Id }, categoryCreate);
            }
            catch 
            {
                return BadRequest("Não foi possível criar a categoria");
             }
        }

        [HttpDelete("{categoryId}/delete")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int categoryId)
        {
            try
            {
                var category = await _categoryCourseRepository.GetCategoryCourseById(categoryId);

                if (category == null)
                    return NotFound("Categoria de curso inexistente");

                await _categoryCourseRepository.DeleteCategory(category);
                return Ok("Categoria excluída!");
            }
            catch
            {
                return BadRequest("Não foi possível excluir a categoria");
            }
        }

    }
}
