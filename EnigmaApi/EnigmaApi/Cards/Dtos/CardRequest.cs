using System.ComponentModel.DataAnnotations;

namespace EnigmaApi.Cards.Dtos
{
    public class CardRequest
    {
        [Required]
        public string Name { get; set; }
        public string? Set { get; set; }
    }
}
