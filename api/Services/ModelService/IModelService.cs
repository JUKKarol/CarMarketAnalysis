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
        Task<ModelDisplayDto> UpdateModel(ModelUpdateDto modelDto);
    }
}
