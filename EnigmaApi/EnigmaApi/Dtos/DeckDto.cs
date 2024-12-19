namespace EnigmaApi.Dtos
{
    public record DeckDto(int Id, string Name, string Description, List<CardDto> Cards);
}
