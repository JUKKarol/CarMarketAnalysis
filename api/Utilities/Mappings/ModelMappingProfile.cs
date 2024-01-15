using AutoMapper;
using CarMarketAnalysis.DTOs.ModelDTOs;
using CarMarketAnalysis.Entities;

namespace CarMarketAnalysis.Utilities.Mappings
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<Model, ModelDetailsDto>().AfterMap((src, dest) => dest.BrandName = src.Brand.Name);
            CreateMap<Model, ModelDisplayDto>().ReverseMap();
            CreateMap<ModelCreateDto, Model>().ReverseMap();
            CreateMap<ModelUpdateDto, Model>().ReverseMap();
        }
    }
}
