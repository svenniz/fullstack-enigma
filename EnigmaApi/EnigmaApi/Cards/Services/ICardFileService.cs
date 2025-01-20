using EnigmaApi.Cards.Models;

namespace EnigmaApi.Cards.Services
{
    public interface ICardFileService
    {
        Task<List<Card>> GetRandomCardsFromFileAsync(string filePath, int numberOfCards);
    }
}