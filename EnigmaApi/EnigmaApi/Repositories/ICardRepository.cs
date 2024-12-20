using EnigmaApi.Models;

namespace EnigmaApi.Repositories
{
    public interface ICardRepository : IRepository<Card>
    {
        Task<IEnumerable<Card>> GetAllCardsAsync();
        Task<Card?> GetCardAsync(int id);
        IQueryable<Card> GetCardWithImage();
    }
}
