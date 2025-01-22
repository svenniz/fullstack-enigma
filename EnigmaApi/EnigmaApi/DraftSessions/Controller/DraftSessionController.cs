using EnigmaApi.DraftSessions.Models;
using EnigmaApi.Shared.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnigmaApi.DraftSessions.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class DraftSessionController : ControllerBase
    {
        //private readonly IDraftSessionService _draftSessionService;
        private readonly IRepository<DraftSession> _draftSessionRepository;
        public DraftSessionController(/*IDraftSessionService draftSessionService,*/ IRepository<DraftSession> draftSessionRepository)
        {
            //_draftSessionService = draftSessionService;
            _draftSessionRepository = draftSessionRepository;
        }
    }
}
