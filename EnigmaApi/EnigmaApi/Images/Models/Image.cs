using EnigmaApi.Cards.Models;
using System.Text.Json.Serialization;

namespace EnigmaApi.Images.Models
{
    public class Image
    {
        public int Id { get; set; }
        public string? Url { get; set; }  // URL for the image
        public string? AltText { get; set; }  // Optional alt text for accessibility
        public int CardId { get; set; }  // Foreign Key to Card
        [JsonIgnore]
        public Card? Card { get; set; } // Navigational Property
    }
}
