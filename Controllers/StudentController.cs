using AutoMapper;
using Gestor_Acadêmico.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController(IStudentRepository studentRepository, IMapper mapper) : ControllerBase
    {
        private readonly IStudentRepository _studentRepository = studentRepository;
        private readonly IMapper _mapper = mapper;
    }
}
