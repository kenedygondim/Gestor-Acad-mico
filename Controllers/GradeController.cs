using AutoMapper;
using Gestor_Acadêmico.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradeController(IGradeRepository gradeRepository, IMapper mapper) : ControllerBase
    {
        private readonly IGradeRepository _gradeRepository = gradeRepository;
        private readonly IMapper _mapper = mapper;
    
    }
}
