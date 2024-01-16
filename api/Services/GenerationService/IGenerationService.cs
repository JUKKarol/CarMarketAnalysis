using CarMarketAnalysis.DTOs.GenerationDTOs;
using CarMarketAnalysis.DTOs.SharedDTOs;
using Sieve.Models;

namespace CarMarketAnalysis.Services.GenerationService
{
    public interface IGenerationService
    {
        Task<GenerationDetailsDto> GetGenerationById(Guid generationId);
        Task<RespondListDto<GenerationDisplayDto>> GetGenerations(SieveModel query);
        Task<GenerationDisplayDto> CreateGeneration(GenerationCreateDto generationDto);
        Task<GenerationDisplayDto> UpdateGeneration(GenerationUpdateDto generationDto);
    }
}
