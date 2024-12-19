using EnigmaApi.Models;

namespace EnigmaApi.Dtos
{
    public record CardDto(int Id, string Name, string ManaCost, string Type, string SetCode, string SetName, string Rarity, int? Power, int? Toughness, string? Description);
}
