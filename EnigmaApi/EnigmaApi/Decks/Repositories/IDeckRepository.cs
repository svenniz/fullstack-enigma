using EnigmaApi.Decks.Models;
using EnigmaApi.Shared.Repositories;

namespace EnigmaApi.Decks.Repositories
{
    public interface IDeckRepository : IRepository<Deck>
    {
        Task<IEnumerable<Deck>> GetAllDeckAsync();
        Task<Deck?> GetDeckAsync(int id);
    }
}