﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using EnigmaApi.Cards.Services;
using EnigmaApi.Cards.Dtos;
using EnigmaApi.Cards.Repositories;
using EnigmaApi.Cards.Models;

namespace EnigmaApi.Cards.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ICardRepository _repository;
        private readonly IMapper _mapper;
        private readonly IScryfallCardService _cardService;

        public CardsController(ICardRepository repository, IMapper mapper, IScryfallCardService cardService)
        {
            _repository = repository;
            _mapper = mapper;
            _cardService = cardService;
        }

        // GET: api/<CardsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardDto>>> GetCardsAsync()
        {
            var cards = await _repository.GetAllCardsAsync();
            if (!cards.Any())
            {
                return NotFound();
            }

            Console.WriteLine($"Number of Cards found: {cards.Count()}");
            var cardDtos = _mapper.Map<IEnumerable<CardDto>>(cards);
            return Ok(cardDtos);
        }

        // GET: api/<CardsController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardDto>> GetCardByIdAsync(int id)
        {
            var card = await _repository.GetCardAsync(id);
            if (card == null)
            {
                return NotFound($"Card with ID {id} not found.");
            }
            var cardDto = _mapper.Map<CardDto>(card);
            return Ok(cardDto);
        }

        // POST: api/<CardsController>
        [HttpPost("manual")]
        public async Task<ActionResult<CardDto>> CreateCardAsync(CardDto cardDto)
        {
            var card = _mapper.Map<Card>(cardDto);

            // Adding card and saving
            _repository.Add(card);
            await _repository.SaveChanges();

            var createdCardDto = _mapper.Map<CardDto>(card);

            return CreatedAtAction(nameof(GetCardByIdAsync), new { id = createdCardDto.Id }, createdCardDto);
        }

        // DELETE: api/<CardsController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardAsync(int id)
        {
            var card = await _repository.Get(id);
            if (card == null)
            {
                return NotFound($"Card with ID {id} not found.");
            }

            _repository.Delete(card);
            await _repository.SaveChanges();
            return NoContent();
        }

        // GET: api/<CardsController>/scryfall/name
        [HttpGet("scryfall/name")]
        public async Task<IActionResult> GetCardFromScryfall([FromQuery] CardRequest cardRequest)
        {
            try
            {
                var card = await _cardService.GetCardDetailsFromScryfall(cardRequest.Name, cardRequest.Set);

                if (card == null)
                {
                    return NotFound($"Cant find Card: {cardRequest.Name} from Set: {cardRequest.Set}");
                }

                return Ok(card);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching card: {ex.Message}");
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // POST: api/<CardsController>/scryfall
        [HttpPost("scryfall")]
        public async Task<ActionResult<CardDto>> CreateCardFromScryfall([FromBody] CardRequest cardRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(cardRequest.Name))
                {
                    return BadRequest("Card name is required");
                }

                var card = await _cardService.GetCardDetailsFromScryfall(cardRequest.Name, cardRequest.Set);
                if (card == null)
                {
                    return NotFound($"Card with name: {cardRequest.Name} not found");
                }

                _repository.Add(card);
                await _repository.SaveChanges();

                var createdCardDto = _mapper.Map<CardDto>(card);
                Console.WriteLine($"CreatedCard ID: {createdCardDto.Id}");

                string url = $"api/Cards/{createdCardDto.Id}";
                Console.WriteLine($"Created card URL: {url}");
                return Created(url, createdCardDto);
                //return CreatedAtAction(nameof(GetCardByIdAsync), new { id = createdCardDto.Id }, createdCardDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
