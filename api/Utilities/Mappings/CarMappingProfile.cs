using AutoMapper;
using CarMarketAnalysis.DTOs.CarDTOs;
using CarMarketAnalysis.Entities;

namespace CarMarketAnalysis.Utilities.Mappings
{
    public class CarMappingProfile : Profile
    {
        public CarMappingProfile()
        {
            CreateMap<CarDisplayDto, Car>().ReverseMap();
            CreateMap<CarDetailsDto, Car>().ReverseMap();
            CreateMap<CarCreateDto, Car>().ReverseMap();
            CreateMap<CarScrapedDTO, Car>().ReverseMap();
            CreateMap<CarScrapedDTO, CarCreateDto>().ReverseMap();
        }
    }
}
