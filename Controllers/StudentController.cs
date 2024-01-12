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
    public class StudentController(IStudentRepository studentRepository, IMapper mapper) : ControllerBase
    {
        private readonly IStudentRepository _studentRepository = studentRepository;
        private readonly IMapper _mapper = mapper;

        readonly string[] situations = ["Matrículado", "Trancado", "Formado", "Desistente", "Afastado"];

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(IEnumerable<StudentDto>))]
        public async Task<IActionResult> GetStudents()
        {
            try
            {
                var students = await _studentRepository.GetStudents();
                var studentsDto = _mapper.Map<List<StudentDto>>(students);
                return Ok(studentsDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar a lista de alunos");
            }
        }

        [HttpGet("{studentId}/id")]
        [ProducesResponseType(200, Type = typeof(StudentDto))]
        public async Task<IActionResult> GetStudentById(int studentId)
        {
            try
            {
                var student = await _studentRepository.GetStudentById(studentId);

                if (student == null)
                    return NotFound("Aluno não encontrado");


                var studentDto = _mapper.Map<StudentDto>(student);
                return Ok(studentDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar o aluno solicitado");
            }
        }

        [HttpGet("{studentName}")]
        [ProducesResponseType(200, Type = typeof(StudentDto))]
        public async Task<IActionResult> GetStudentByName(string studentName)
        {
            try
            {
                var student = await _studentRepository.GetStudentByName(studentName);

                if (student == null)
                    return NotFound("Aluno não encontrado");

                var studentDto = _mapper.Map<StudentDto>(student);
                return Ok(studentDto);
            }
            catch
            {
                return BadRequest("Não foi possível recuperar o curso solicitado");
            }
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(StudentDto))]
        public async Task<IActionResult> CreateStudent(Student student)
        {
            try
            {
                if (student == null || !ModelState.IsValid)
                    return BadRequest("Insira os dados corretamente");

                if (!situations.Contains(student.Status, StringComparer.OrdinalIgnoreCase))
                    return BadRequest("Insira uma situação válida: 'Matrículado', 'Trancado', 'Formado', 'Desistente', 'Afastado'");

                if (student.Cpf.Length != 11)
                    return BadRequest("O campo CPF deve ter 11 dígitos sem pontos ou traços");

                await _studentRepository.CreateStudent(student);

                var StudentDto = _mapper.Map<StudentDto>(student);

                return Ok(StudentDto);
            }
            catch
            {
                return BadRequest("Não foi possível criar o curso solicitado");
            }
        }


    }
}
