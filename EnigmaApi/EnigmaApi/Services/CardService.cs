namespace EnigmaApi.Services
{
    using System.Net.Http;
    using EnigmaApi.Models;
    using Newtonsoft.Json;

    public class CardService
    {
        private readonly HttpClient _httpClient;

        public CardService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Card> GetCardDetailsFromScryfall(string cardName)
        {
            var response = await _httpClient.GetStringAsync($"https://api.scryfall.com/cards/named?fuzzy={cardName}");
            var cardData = JsonConvert.DeserializeObject<ScryfallCard>(response);

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
                new Image { Url = cardData.ImageUris.Normal, AltText = $"{cardData.Name} - Artwork" }
            }
            };

            return card;
        }
    }

    public class ScryfallCard
    {
        public string Name { get; set; }
        public string ManaCost { get; set; }
        public string Type { get; set; }
        public string Set { get; set; }
        public string Rarity { get; set; }
        public int? Power { get; set; }
        public int? Toughness { get; set; }
        public string Description { get; set; }
        public ImageUris ImageUris { get; set; }
    }

    public class ImageUris
    {
        public string Normal { get; set; }
    }

}
