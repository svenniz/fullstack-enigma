namespace EnigmaApi.Services
{
    using System.ComponentModel.DataAnnotations;
    using System.Net.Http;
    using System.Text.Json;
    using EnigmaApi.Models;
    using Newtonsoft.Json;

    public class CardService : ICardService
    {
        private readonly HttpClient _httpClient;

        public CardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Card> GetCardDetailsFromScryfall(string cardName)
        {
            try
            {
                // Setting headers for request:
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "EnigmaApi/1.0");
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                // Send Request to Scryfall
                var url = $"https://api.scryfall.com/cards/named?fuzzy={Uri.EscapeDataString(cardName)}";
                Console.WriteLine($"Fetching card from {url}");
                var response = await _httpClient.GetAsync(url);
                Console.WriteLine($"Response Status Code: {response.StatusCode}");

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error: {response.StatusCode} - Reason: {response.ReasonPhrase} - {errorContent}");
                    throw new Exception($"Failed to retrieve card details. Status code: {response.StatusCode}");
                }

                // Parse response as JSON
                var json = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"Response Content: {json}");
                var cardData = JsonConvert.DeserializeObject<ScryfallCard>(json);

                // Map Scryfall Card to Card Model
                var card = new Card
                {
                    Name = cardData.Name,
                    ManaCost = cardData.ManaCost,
                    Type = cardData.Type,
                    SetCode = cardData.Set,
                    Rarity = cardData.Rarity,
                    Power = cardData.Power,
                    Toughness = cardData.Toughness,
                    Description = cardData.Description,
                    Images = new List<Image>
                    {
                        new Image {Url=cardData.ImageUris?.Normal}
                    }.Where(i => !string.IsNullOrWhiteSpace(i.Url)).ToList()
                };

                return card;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error while fetching card from scryfall: {ex.Message}");
                throw;
            }

        }
    }
    public class ScryfallCard
    {
        public string Name { get; set; }
        public string? ManaCost { get; set; }
        public string? Type { get; set; }
        public string? Set { get; set; }
        public string? Rarity { get; set; }
        public int? Power { get; set; }
        public int? Toughness { get; set; }
        public string? Description { get; set; }
        public ImageUris ImageUris { get; set; }
    }
    public class ImageUris
    {
        public string Normal { get; set; }
    }
}
