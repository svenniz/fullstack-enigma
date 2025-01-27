using EnigmaApi.Boosters.Models;
using EnigmaApi.Boosters.Services;
using EnigmaApi.Shared.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnigmaApi.Boosters.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoosterController : ControllerBase
    {
        private readonly IBoosterService _boosterService;
        private readonly IRepository<Booster> _boosterRepository;
        public BoosterController(IBoosterService boosterService, IRepository<Booster> boosterRepository)
        {
            _boosterService = boosterService;
            _boosterRepository = boosterRepository;
        }

        // POST: api/<BoosterController>
        [HttpPost]
        public async Task<ActionResult<Booster>> CreateBoosterAsync()
        {
            var booster = await _boosterService.CreateBoosterAsync();
            return Ok(booster);
        }
    }
}
