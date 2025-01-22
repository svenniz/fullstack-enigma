using AutoMapper;
using EnigmaApi.Decks.Dtos;
using EnigmaApi.Decks.Models;
using EnigmaApi.Decks.Repositories;
using EnigmaApi.Decks.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace EnigmaApi.Decks.Controller
{
    [Route("api/[controller]")]
    [ApiController]

    public class DeckController : ControllerBase
    {
        private readonly IDeckRepository _repository;
        private readonly IMapper _mapper;
        private readonly IDeckService _deckService;

        public DeckController(IDeckRepository repository, IMapper mapper, IDeckService deckService)
        {
            _repository = repository;
            _mapper = mapper;
            _deckService = deckService;
        }

        // Get all decks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeckDto>>> GetDecksAsync()
        {
            var decks = await _repository.GetAllDeckAsync();
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

        // Add card to deck
        [HttpPost("{deckId}/add-card/{cardId}")]
        public async Task<IActionResult> AddCardToDeck(int deckId, int cardId)
        {
            try
            {
                await _deckService.AddCardToDeckAsync(deckId, cardId);
                await _repository.SaveChanges();
                return Ok("Card added to deck");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // Export deck
        [HttpGet("{id}/export")]
        public async Task<IActionResult> ExportDeckAsync(int id)
        {
            // Fetch deck
            var deck = await _repository.GetDeckAsync(id);
            if (deck == null)
            {
                return NotFound($"Deck with ID {id} not found.");
            }

            // Generate deck export
            var deckExport = _deckService.GenerateDeckExport(deck);

            // Create file
            var fileName = $"{deck.Name}.txt";
            return File(
                Encoding.UTF8.GetBytes(deckExport),
                "text/plain",
                fileName
                );
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
            if (deck == null)
            {
                return NotFound();
            }
            _repository.Delete(deck);
            await _repository.SaveChanges();

            return Ok();
        }
    }
}
