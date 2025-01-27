using EnigmaApi.Cards.Dtos;

namespace EnigmaApi.Boosters.Dtos
{
    public class BoosterDto
    {
        public List<CardDto> Cards { get; set; } = new List<CardDto>();
    }
}
