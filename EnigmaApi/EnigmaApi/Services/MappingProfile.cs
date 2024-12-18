using AutoMapper;
using EnigmaApi.Dtos;
using EnigmaApi.Models;

namespace EnigmaApi.Services
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Card, CardDto>().ReverseMap();
        }
    }
}
