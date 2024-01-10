using AutoMapper;
using Gestor_Acadêmico.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;

namespace Gestor_Acadêmico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnController(ITurnRepository turnRepository, IMapper mapper) : ControllerBase
    { 
        private readonly ITurnRepository _turnRepository = turnRepository;
        private readonly IMapper _mapper = mapper;
    }
}
