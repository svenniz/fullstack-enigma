using EnigmaApi.Decks.Models;

namespace EnigmaApi.Decks.Services
{
    public interface IDeckService
    {
        string GenerateDeckExport(Deck deck);
    }
}