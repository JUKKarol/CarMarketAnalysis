using AutoMapper;
using CarMarketAnalysis.DTOs.BrandDTOs;
using CarMarketAnalysis.Entities;

namespace CarMarketAnalysis.Utilities.Mappings
{
    public class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<BrandDisplayDto, Brand>().ReverseMap();
            CreateMap<Brand, BrandDetailsDto>().ForMember(dest => dest.Models, opt => opt.MapFrom(src => src.Models));
            CreateMap<BrandCreateDto, Brand>().ReverseMap();
            CreateMap<BrandUpdateDto, Brand>().ReverseMap();
        }
    }
}