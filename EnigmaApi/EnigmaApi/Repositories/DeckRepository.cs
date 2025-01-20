using EnigmaApi.Data_Access;
using Microsoft.EntityFrameworkCore;
using EnigmaApi.Models;
using EnigmaApi.Dtos;
using AutoMapper;

namespace EnigmaApi.Repositories
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

        public IQueryable<Deck> GetDeckWithCardAsync()
        {
            return _context.Decks
                .Include(d => d.DeckCards)
                .ThenInclude(dc => dc.Card);
        }

        public async Task<IEnumerable<Deck>> GetAllDeckDtos()
        {
            return await GetDeckWithCardAsync().ToListAsync();
        }
    }
}
