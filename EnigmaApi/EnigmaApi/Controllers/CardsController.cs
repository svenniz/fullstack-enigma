using EnigmaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnigmaApi.Repositories;
using EnigmaApi.Dtos;
using AutoMapper;
using EnigmaApi.Services;

namespace EnigmaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly IRepository<Card> _repository;
        private readonly IMapper _mapper;
        private readonly ICardService _cardService;

        public CardsController(IRepository<Card> repository, IMapper mapper, ICardService cardService)
        {
            _repository = repository;
            _mapper = mapper;
            _cardService = cardService;
        }

        // GET: api/<CardsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardDto>>> GetCardsAsync()
        {
            var cards = await _repository.GetAll();
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
        public async Task<ActionResult<CardDto>> GetCardAsync(int id)
        {
            var card = await _repository.Get(id);
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

            return CreatedAtAction(nameof(GetCardAsync), new { id = createdCardDto.Id }, createdCardDto);
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
        [HttpGet("scryfall/{cardName}")]
        public async Task<IActionResult> GetCardByName(string cardName)
        {
            try
            {
                var card = await _cardService.GetCardDetailsFromScryfall(cardName);

                if ((card == null))
                {
                    return NotFound($"Cant find Card: {cardName}");
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
        public async Task<ActionResult<Card>> CreateCardFromScryfall([FromBody] string cardName)
        {
            try
            {
                if (string.IsNullOrEmpty(cardName))
                {
                    return BadRequest("Card name is required");
                }

                var card = await _cardService.GetCardDetailsFromScryfall(cardName);
                if (card == null)
                {
                    return NotFound($"Card with name: {cardName} not found");
                }

                _repository.Add(card);
                await _repository.SaveChanges();

                var createdCardDto = _mapper.Map<CardDto>(card);

                return CreatedAtAction(nameof(GetCardAsync), new {id = card.Id }, createdCardDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error: {ex.Message}");
            }
        }
    }
}
