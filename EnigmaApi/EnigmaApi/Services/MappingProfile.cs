using AutoMapper;
using EnigmaApi.Dtos;
using EnigmaApi.Models;

namespace EnigmaApi.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            // Map from Card to CardDto and back
            CreateMap<Card, CardDto>().ReverseMap();

            // Map from Deck to DeckDto
            CreateMap<Deck, DeckDto>()
                .ForMember(dest => dest.Cards, opt =>
                opt.MapFrom(src => src.DeckCards.Select(dc => dc.Card)));

            // Map from DeckDto to Deck
            CreateMap<DeckDto, Deck>()
                .ForMember(dest => dest.DeckCards, opt =>
                opt.MapFrom(src => src.Cards.Select(cardDto => new DeckCard
                {
                    CardId = cardDto.Id,
                })));
        }
    }
}
