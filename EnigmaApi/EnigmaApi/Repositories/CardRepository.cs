using EnigmaApi.Data_Access;
using EnigmaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace EnigmaApi.Repositories
{
    public class CardRepository : GenericEfCoreRepository<Card>, ICardRepository
    {
        private readonly EnigmaDbContext _context;

        public CardRepository(EnigmaDbContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<Card> GetCardWithImage()
        {
            return _context.Cards
                .Include(c => c.Images);
        }
        public async Task<Card?> GetCardAsync(int id)
        {
            return await GetCardWithImage()
                .FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<IEnumerable<Card>> GetAllCardsAsync()
        {
            return await GetCardWithImage().ToListAsync();
        }
    }
}
