using EnigmaApi.Dtos;
using EnigmaApi.Models;

namespace EnigmaApi.Repositories
{
    public interface IDeckRepository : IRepository<Deck>
    {
        Task<IEnumerable<Deck>> GetAllDeckDtos();
        IQueryable<Deck> GetDeckWithCardAsync();
    }
}