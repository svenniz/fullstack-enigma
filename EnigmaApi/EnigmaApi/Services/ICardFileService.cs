using EnigmaApi.Models;

namespace EnigmaApi.Services
{
    public interface ICardFileService
    {
        Task<List<Card>> GetRandomCardsFromFileAsync(string filePath, int numberOfCards);
    }
}