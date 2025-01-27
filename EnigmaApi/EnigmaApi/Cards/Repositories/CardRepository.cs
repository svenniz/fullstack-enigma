using EnigmaApi.Cards.Models;
using EnigmaApi.Cards.Repositories;
using EnigmaApi.Data_Access;
using EnigmaApi.Shared.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EnigmaApi.Cards.Repositories
{
    public class CardRepository : GenericEfCoreRepository<Card>, ICardRepository
    {
        private readonly EnigmaDbContext _context;

        public CardRepository(EnigmaDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Card?> GetCardAsync(int id)
        {
            return await _context.Cards
                .Include(c => c.Images)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await _context.Cards
                .Include(c => c.Images).ToListAsync();
        }
        public async Task<IEnumerable<Card>> GetCardsByNames(IEnumerable<string> names)
        {
            return await _context.Cards
                .Include(c => c.Images)
                .Where(c => names.Contains(c.Name))
                .ToListAsync();
        }
    }
}
