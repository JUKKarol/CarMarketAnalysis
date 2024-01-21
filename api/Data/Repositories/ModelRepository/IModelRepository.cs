using CarMarketAnalysis.Entities;
using Sieve.Models;

namespace CarMarketAnalysis.Data.Repositories.ModelRepository
{
    public interface IModelRepository
    {
        Task<Model> GetModelById(Guid modelId);
        Task<List<Model>> GetModels(SieveModel query);
        Task<int> GetModelsCount(SieveModel query);
        Task<Model> CreateModel(Model model);
        Task<List<Model>> CreateModels(List<Model> models);
        Task<Model> UpdateModel(Model updatedModel);
    }
}
