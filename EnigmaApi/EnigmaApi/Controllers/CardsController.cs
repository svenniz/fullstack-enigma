using EnigmaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EnigmaApi.Repositories;
using EnigmaApi.Dtos;

namespace EnigmaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly IRepository<Card> _repository;

        public CardsController(IRepository<Card> repository)
        {
            _repository = repository;
        }

        // GET: api/<CardsController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCardsAsync()
        {
            var cards = await _repository.GetAll();
            if (!cards.Any())
            {
                return NotFound();
            }
            Console.WriteLine($"Number of Cards found: {cards.Count()}");
            return Ok(cards);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Card>> GetCardAsync(int id)
        {
            var card = await _repository.Get(id);
            if (card == null)
            {
                return NotFound($"Card with ID {id} not found.");
            }

            return Ok(card);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCardAsync(Card card)
        {
            _repository.Add(card);
            await _repository.SaveChanges();
            return CreatedAtAction(nameof(GetCardAsync), new { id = card.Id }, card);
        }

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
    }
}
