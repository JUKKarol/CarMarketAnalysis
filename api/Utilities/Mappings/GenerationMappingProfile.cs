using AutoMapper;
using CarMarketAnalysis.DTOs.GenerationDTOs;
using CarMarketAnalysis.DTOs.ModelDTOs;
using CarMarketAnalysis.Entities;

namespace CarMarketAnalysis.Utilities.Mappings
{
    public class GenerationMappingProfile : Profile
    {
        public GenerationMappingProfile()
        {
            CreateMap<Generation, GenerationDetailsDto>().ReverseMap();
            CreateMap<Generation, GenerationDisplayDto>().ReverseMap();
            CreateMap<GenerationCreateDto, Generation>().ReverseMap();
            CreateMap<GenerationUpdateDto, Generation>().ReverseMap();
        }
    }
}
