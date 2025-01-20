using EnigmaApi.Images.Dtos;
using EnigmaApi.Models;

namespace EnigmaApi.Cards.Dtos
{
    public record CardDto(int Id, string Name, string ManaCost, string Type, int? Power, int? Toughness, string? Description, string? Cmc, string SetCode, string SetName, string? Language, string Rarity, string? ArtistName, string? ReleasedAt, ICollection<ImageDto> Images);
}