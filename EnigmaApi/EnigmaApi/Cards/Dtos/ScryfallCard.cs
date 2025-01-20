using EnigmaApi.Cards.Services;
using EnigmaApi.Images.Dtos;
using Newtonsoft.Json;

namespace EnigmaApi.Cards.Dtos
{
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
        public string? Cmc { get; set; }
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
}
