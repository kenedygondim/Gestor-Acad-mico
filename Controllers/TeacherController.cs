using AutoMapper;
using Gestor_Acadêmico.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeacherController(ITeacherRepository teacherRepository, IMapper mapper) : ControllerBase
    { 

        private readonly ITeacherRepository _teacherRepository = teacherRepository;
        private readonly IMapper _mapper = mapper;
    
    }
}
