namespace EnigmaApi.Dtos
{
    public class DeckDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public ICollection<CardDto> Cards { get; set; } = new List<CardDto>();
    }
}
