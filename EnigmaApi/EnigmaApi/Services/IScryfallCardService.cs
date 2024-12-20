using EnigmaApi.Models;

namespace EnigmaApi.Services
{
    public interface IScryfallCardService
    {
        Task<Card> GetCardDetailsFromScryfall(string cardName, string? set = null);
    }
}