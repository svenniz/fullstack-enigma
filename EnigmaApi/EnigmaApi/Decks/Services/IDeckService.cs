using EnigmaApi.Decks.Models;

namespace EnigmaApi.Decks.Services
{
    public interface IDeckService
    {
        Task AddCardToDeckAsync(int deckId, int cardId);
        string GenerateDeckExport(Deck deck);
    }
}