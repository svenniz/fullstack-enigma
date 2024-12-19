namespace EnigmaApi.Dtos
{
    public record CardDto(int Id, string Name, string ManaCost, string Type, int? Power, int? Toughness, string? Description);

}
