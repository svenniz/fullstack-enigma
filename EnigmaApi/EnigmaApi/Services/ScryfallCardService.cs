namespace EnigmaApi.Services
{
    using System.ComponentModel.DataAnnotations;
    using System.Net.Http;
    using System.Text.Json;
    using EnigmaApi.Models;
    using Newtonsoft.Json;

    public class ScryfallCardService : IScryfallCardService
    {
        private readonly HttpClient _httpClient;

        public ScryfallCardService(HttpClient httpClient)
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
        public async Task<Card> GetCardDetailsFromScryfall(string cardName, string? set = null)
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
                var card = MapToCard(cardData);

                return card;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"error while fetching card from scryfall: {ex.Message}");
                throw;
            }
        }
        /// <summary>
        /// Abstracting the mapping logic to seperate function
        /// </summary>
        /// <param name="cardData"></param>
        /// <returns></returns>
        private Card MapToCard(ScryfallCard cardData)
        {
            return new Card
            {
                Name = cardData.Name ?? "Unknown Name",
                ManaCost = cardData.ManaCost,
                Type = cardData.TypeLine,
                Power = cardData.Power,
                Toughness = cardData.Toughness,
                Cmc = cardData.Cmc,
                Description = cardData.OracleText,
                SetCode = cardData.Set,
                SetName = cardData.SetName,
                Language = cardData.Language,
                Rarity = cardData.Rarity,
                ArtistName = cardData.ArtistName,
                ReleasedAt = cardData.ReleasedAt,
                Images = MapToImages(cardData.ImageUris)
            };
        }
        /// <summary>
        /// Seperate image mapping for card
        /// </summary>
        /// <param name="imageUris"></param>
        /// <returns></returns>
        private List<Image> MapToImages(ImageUris imageUris)
        {
            return imageUris != null && !string.IsNullOrWhiteSpace(imageUris.Normal)
                ? new List<Image> { new Image { Url = imageUris.Normal } }
                : new List<Image>();
        }
    }

    /// <summary>
    /// Dto-like object for using Scryfall Api JsonProperties and mapping them to object structure
    /// </summary>
    public class ScryfallCard
    {
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("mana_cost")]
        public string? ManaCost { get; set; }
        [JsonProperty("type_line")]
        public string? TypeLine { get; set; }
        [JsonProperty("power")]
        public int? Power { get; set; }
        [JsonProperty("toughness")]
        public int? Toughness { get; set; }
        [JsonProperty("oracle_text")]
        public string? OracleText { get; set; }
        [JsonProperty("cmc")]
        public string? Cmc {  get; set; }
        [JsonProperty("set")]
        public string? Set { get; set; }
        [JsonProperty("set_name")]
        public string? SetName { get; set; }
        [JsonProperty("lang")]
        public string? Language { get; set; }
        [JsonProperty("rarity")]
        public string? Rarity { get; set; }
        [JsonProperty("artist")]
        public string? ArtistName { get; set; }
        [JsonProperty("released_at")]
        public string? ReleasedAt { get; set; }
        [JsonProperty("image_uris")]
        public ImageUris? ImageUris { get; set; }
    }
    public class ImageUris
    {
        public string? Normal { get; set; }
    }
}
