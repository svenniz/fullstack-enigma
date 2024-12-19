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
        /// <summary>
        /// Uses Scryfall Api to search for card using fuzzy search. 
        /// Gets JSON object that maps to ScryfallCard object, that is then mapped to Card Entity
        /// </summary>
        /// <param name="cardName">fuzzy card name</param>
        /// <param name="set">nullable set. Default is set by scryfall</param>
        /// <returns>Card entity model for database</returns>
        public async Task<Card> GetCardDetailsFromScryfall(string cardName, string set = null)
        {
            try
            {
                // Setting headers for request
                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "EnigmaApi/1.0");
                _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                // Setup url
                var baseUrl = "https://api.scryfall.com/cards/named";
                var url = $"{baseUrl}?fuzzy={Uri.EscapeDataString(cardName)}";
                Console.WriteLine($"Fetching card from {url}");
                if(!string.IsNullOrEmpty(set))
                {
                    url += $"&set={set}";
                }

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
                    Name = cardData.Name ?? "Unknown Name",
                    ManaCost = cardData.ManaCost,
                    Type = cardData.TypeLine,
                    SetCode = cardData.Set,
                    Rarity = cardData.Rarity,
                    Power = cardData.Power ?? null,
                    Toughness = cardData.Toughness ?? null,
                    Description = cardData.OracleText,
                    Images = cardData.ImageUris != null && !string.IsNullOrWhiteSpace(cardData.ImageUris.Normal)
                    ? new List<Image> { new Image { Url=cardData.ImageUris.Normal } }
                    : new List<Image>()
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
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("mana_cost")]
        public string? ManaCost { get; set; }
        [JsonProperty("type_line")]
        public string? TypeLine { get; set; }
        [JsonProperty("set")]
        public string? Set { get; set; }
        [JsonProperty("rarity")]
        public string? Rarity { get; set; }
        [JsonProperty("power")]
        public int? Power { get; set; }
        [JsonProperty("toughness")]
        public int? Toughness { get; set; }
        [JsonProperty("oracle_text")]
        public string? OracleText { get; set; }
        [JsonProperty("image_uris")]
        public ImageUris ImageUris { get; set; }
    }
    public class ImageUris
    {
        public string Normal { get; set; }
    }
}
