using AutoMapper;
using Gestor_Acadêmico.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController(ISubjectRepository subjectRepository, IMapper mapper) : ControllerBase
    {
        private readonly ISubjectRepository _subjectRepository = subjectRepository;
        private readonly IMapper _mapper = mapper;
    }
    
}
