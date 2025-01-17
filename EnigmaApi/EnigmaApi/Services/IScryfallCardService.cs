using EnigmaApi.Models;

namespace EnigmaApi.Services
{
    public interface IScryfallCardService
    {
        Task<Card> GetCardDetailsFromScryfall(string cardName, string? set = null);
        Task<List<Card>> GetCardsDetailsFromScryfall(List<string> cardNames, string? set = null);
    }
}