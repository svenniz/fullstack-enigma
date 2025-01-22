using EnigmaApi.Cards.Models;
using EnigmaApi.Cards.Services;

namespace EnigmaApi.Cards.Services
{
    public class CardFileService : ICardFileService
    {
        private readonly IScryfallCardService _scryfallCardService;  // Service for fetching cards from Scryfall

        public CardFileService(IScryfallCardService scryfallCardService)
        {
            _scryfallCardService = scryfallCardService;
        }

        /// <summary>
        /// Returns a random selection of cards from a file
        /// </summary>
        /// <param name="filePath">a txt file</param>
        /// <param name="numberOfCards">probably 15</param>
        /// <returns></returns>
        public async Task<List<Card>> GetRandomCardsFromFileAsync(string filePath, int numberOfCards)
        {
            var cardNames = await ReadCardNamesFromFileAsync(filePath);
            var selectedCardNames = cardNames.OrderBy(c => Guid.NewGuid()).Take(numberOfCards).ToList();

            var cards = await _scryfallCardService.GetCardsDetailsFromScryfall(selectedCardNames);

            return cards;
        }

        /// <summary>
        /// Read lines from txt file and return a list of card names
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
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