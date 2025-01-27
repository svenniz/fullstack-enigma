using EnigmaApi.Boosters.Models;
using EnigmaApi.Boosters.Services;
using EnigmaApi.Shared.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EnigmaApi.Boosters.Dtos;

namespace EnigmaApi.Boosters.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class BoosterController : ControllerBase
    {
        private readonly IBoosterService _boosterService;
        private readonly IRepository<Booster> _boosterRepository;
        private readonly IMapper _mapper;
        public BoosterController(IBoosterService boosterService, IRepository<Booster> boosterRepository, IMapper mapper)
        {
            _boosterService = boosterService;
            _boosterRepository = boosterRepository;
            _mapper = mapper;
        }

        // Endpoint to create a booster
        [HttpGet("create")]
        public async Task<ActionResult<BoosterDto>> CreateBooster([FromQuery] string filePath = null)
        {
            try
            {
                // Call the service to create a booster
                var booster = await _boosterService.CreateBoosterAsync(filePath);

                var boosterDto = _mapper.Map<Booster>(booster);
                // Return the booster as JSON
                return Ok(boosterDto);
            }
            catch (Exception ex)
            {
                // Handle any errors
                return StatusCode(500, new { message = "Error creating booster", error = ex.Message });
            }
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
