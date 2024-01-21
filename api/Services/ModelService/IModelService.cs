using CarMarketAnalysis.DTOs.ModelDTOs;
using CarMarketAnalysis.DTOs.SharedDTOs;
using Sieve.Models;

namespace CarMarketAnalysis.Services.ModelService
{
    public interface IModelService
    {
        Task<ModelDetailsDto> GetModelById(Guid modelId);
        Task<RespondListDto<ModelDisplayDto>> GetModels(SieveModel query);
        Task<ModelDisplayDto> CreateModel(ModelCreateDto modelDto);
        Task<List<ModelDisplayDto>> CreateModels(List<ModelCreateDto> modelsDto);
        Task<ModelDisplayDto> UpdateModel(ModelUpdateDto modelDto);
    }
}
