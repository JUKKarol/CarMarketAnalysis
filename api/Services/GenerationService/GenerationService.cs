using AutoMapper;
using CarMarketAnalysis.Data.Repositories.GenerationRepository;
using CarMarketAnalysis.DTOs.GenerationDTOs;
using CarMarketAnalysis.DTOs.SharedDTOs;
using CarMarketAnalysis.Entities;
using Sieve.Models;

namespace CarMarketAnalysis.Services.GenerationService
{
    public class GenerationService(
        IGenerationRepository generationRepository,
        IMapper mapper) : IGenerationService
    {
        public async Task<GenerationDetailsDto> GetGenerationById(Guid generationId)
        {
            var generation = await generationRepository.GetGenerationById(generationId);
            return mapper.Map<GenerationDetailsDto>(generation);
        }

        public async Task<RespondListDto<GenerationDisplayDto>> GetGenerations(SieveModel query)
        {
            int pageSize = query.PageSize != null ? (int)query.PageSize : 40;

            var generations = await generationRepository.GetGenerations(query);
            var generationsDto = mapper.Map<List<GenerationDisplayDto>>(generations);

            RespondListDto<GenerationDisplayDto> respondListDto = new();
            respondListDto.Items = generationsDto;
            respondListDto.ItemsCount = await generationRepository.GetGenerationsCount(query);
            respondListDto.PagesCount = (int)Math.Ceiling((double)respondListDto.ItemsCount / pageSize);

            return respondListDto;
        }

        public async Task<GenerationDisplayDto> CreateGeneration(GenerationCreateDto generationDto)
        {
            var generation = mapper.Map<Generation>(generationDto);
            await generationRepository.CreateGeneration(generation);

            return mapper.Map<GenerationDisplayDto>(generation);
        }

        public async Task<GenerationDisplayDto> UpdateGeneration(GenerationUpdateDto generationDto)
        {
            var generation = mapper.Map<Generation>(generationDto);
            await generationRepository.UpdateGeneration(generation);

            return mapper.Map<GenerationDisplayDto>(generation);
        }
    }
}
