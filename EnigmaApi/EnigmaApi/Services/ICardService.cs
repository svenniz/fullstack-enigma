using EnigmaApi.Models;

namespace EnigmaApi.Services
{
    public interface ICardService
    {
        Task<Card> GetCardDetailsFromScryfall(string cardName);
    }
}