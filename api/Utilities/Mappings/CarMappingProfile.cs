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
            CreateMap<Car, CarDetailsDto>()
                .ForMember(c => c.Model, opt =>
                opt.MapFrom(src => src.Model.Name))
                .ForMember(d => d.Brand, opt =>
                opt.MapFrom(src => src.Model.Brand.Name));
            CreateMap<CarCreateDto, Car>().ReverseMap();
            CreateMap<CarScrapedDTO, Car>().ReverseMap();
            CreateMap<CarScrapedDTO, CarCreateDto>().ReverseMap();
        }
    }
}