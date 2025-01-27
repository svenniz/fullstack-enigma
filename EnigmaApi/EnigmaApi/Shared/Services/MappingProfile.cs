using AutoMapper;
using EnigmaApi.Boosters.Dtos;
using EnigmaApi.Boosters.Models;
using EnigmaApi.Cards.Dtos;
using EnigmaApi.Cards.Models;
using EnigmaApi.DeckCards.Models;
using EnigmaApi.Decks.Dtos;
using EnigmaApi.Decks.Models;
using EnigmaApi.Images.Dtos;
using EnigmaApi.Images.Models;

namespace EnigmaApi.Shared.Services
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

            // Map from Image to ImageDto and back
            CreateMap<Image, ImageDto>().ReverseMap();

            // Map from Booster to BoosterDto and back
            CreateMap<Booster, BoosterDto>()
                .ForMember(dest => dest.Cards, opt =>
                opt.MapFrom(src => src.Cards));
        }
    }
}