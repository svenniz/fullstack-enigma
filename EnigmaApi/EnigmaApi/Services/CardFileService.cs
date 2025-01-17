using EnigmaApi.Models;

namespace EnigmaApi.Services
{

    public class CardFileService : ICardFileService
    {
        private readonly IScryfallCardService _scryfallCardService;  // Service for fetching cards from Scryfall

        public CardFileService(IScryfallCardService scryfallCardService)
        {
            _scryfallCardService = scryfallCardService;
        }

        public async Task<List<Card>> GetRandomCardsFromFileAsync(string filePath, int numberOfCards)
        {
            var cardNames = await ReadCardNamesFromFileAsync(filePath);
            var selectedCardNames = cardNames.OrderBy(c => Guid.NewGuid()).Take(numberOfCards).ToList();

            var cards = await _scryfallCardService.GetCardsDetailsFromScryfall(selectedCardNames);

            return cards;
        }

        private async Task<List<string>> ReadCardNamesFromFileAsync(string filePath)
        {
            var cardNames = new List<string>();

            // Read the file content (assuming it's a .txt file with one card name per line)
            var lines = await File.ReadAllLinesAsync(filePath);

            foreach (var line in lines)
            {
                var cardName = line.Trim();
                if (!string.IsNullOrEmpty(cardName))
                {
                    cardNames.Add(cardName);
                }
            }

            return cardNames;
        }
    }

}
