namespace EnigmaApi.Dtos
{
    public record CardDto(int Id, string Name, string ManaCost, string Type);
    public record CreateCardDto(string Name, string ManaCost, string Type);

}
