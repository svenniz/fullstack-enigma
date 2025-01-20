using AutoMapper;
using EnigmaApi.Dtos;
using EnigmaApi.Models;
using EnigmaApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnigmaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class DeckController : ControllerBase
    {
        private readonly IDeckRepository _repository;
        private readonly IMapper _mapper;

        public DeckController(IDeckRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // Get all decks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeckDto>>> GetDecksAsync()
        {
            var decks = await _repository.GetAllDeckDtos();
            if (!decks.Any())
            {
                return NotFound();
            }
            var deckDtos = _mapper.Map<IEnumerable<DeckDto>>(decks);
            return Ok(deckDtos);
        }

        // Get Deck by Id
        [HttpGet("{id}")]
        public async Task<ActionResult<DeckDto>> GetDeckByIdAsync(int id)
        {
            var deck = await _repository.Get(id);
            if (deck == null)
            {
                return NotFound();
            }
            var deckDto = _mapper.Map<DeckDto>(deck);
            return Ok(deckDto);
        }

        // Create deck
        [HttpPost]
        public async Task<ActionResult<DeckDto>> CreateDeckAsync(DeckDto deckDto)
        {
            var deck = _mapper.Map<Deck>(deckDto);

            // Adding deck
            _repository.Add(deck);
            await _repository.SaveChanges();
            var createdDeckDto = _mapper.Map<DeckDto>(deck);
            return CreatedAtAction(nameof(GetDeckByIdAsync), new { id = deck.Id }, createdDeckDto);
        }

        // Update deck
        [HttpPut("{id}")]
        public async Task<ActionResult<DeckDto>> UpdateDeckAsync(int id, DeckDto deckDto)
        {
            if (id != deckDto.Id)
            {
                return BadRequest("Id mismatch");
            }

            var existingDeck = await _repository.Get(id);
            if (existingDeck == null)
            {
                return NotFound();
            }

            // Mapping Dto to entity
            _mapper.Map(deckDto, existingDeck);

            // Update
            _repository.Update(existingDeck);
            await _repository.SaveChanges();

            var updatedDeck = _mapper.Map<DeckDto>(existingDeck);
            return Ok(updatedDeck);
        }

        // Delete deck
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeckAsync(int id)
        {
            var deck = await _repository.Get(id);
            if(deck == null)
            {
                return NotFound();
            }
            _repository.Delete(deck);
            await _repository.SaveChanges();

            return Ok();
        }
    }
}
