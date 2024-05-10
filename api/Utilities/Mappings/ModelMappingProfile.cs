using AutoMapper;
using CarMarketAnalysis.DTOs.ModelDTOs;
using CarMarketAnalysis.Entities;

namespace CarMarketAnalysis.Utilities.Mappings
{
    public class ModelMappingProfile : Profile
    {
        public ModelMappingProfile()
        {
            CreateMap<Model, ModelDetailsDto>().ReverseMap();
            CreateMap<Model, ModelDisplayDto>().ReverseMap();
            CreateMap<ModelCreateDto, Model>().ReverseMap();
            CreateMap<ModelUpdateDto, Model>().ReverseMap();
        }
    }
}