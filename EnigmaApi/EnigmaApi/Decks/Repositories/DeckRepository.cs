using EnigmaApi.Data_Access;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using EnigmaApi.Decks.Models;
using EnigmaApi.Shared.Repositories;

namespace EnigmaApi.Decks.Repositories
{
    public class DeckRepository : GenericEfCoreRepository<Deck>, IDeckRepository
    {
        private readonly EnigmaDbContext _context;
        private readonly IMapper _mapper;

        public DeckRepository(EnigmaDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public IQueryable<Deck> GetDeckWithCardsAsync()
        {
            return _context.Decks
                .Include(d => d.DeckCards)
                .ThenInclude(dc => dc.Card);
        }

        public async Task<IEnumerable<Deck>> GetAllDeckDtos()
        {
            return await GetDeckWithCardsAsync().ToListAsync();
        }
        public async Task<Deck?> GetDeckAsync(int id)
        {
            return await GetDeckWithCardsAsync()
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
