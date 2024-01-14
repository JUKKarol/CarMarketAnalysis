using CarMarketAnalysis.Entities;
using Sieve.Models;

namespace CarMarketAnalysis.Data.Repositories.GenerationRepository
{
    public interface IGenerationRepository
    {
        Task<Generation> GetGenerationById(Guid generationId);
        Task<List<Generation>> GetGenerations(SieveModel query);
        Task<int> GetGenerationsCount(SieveModel query);
        Task<Generation> CreateGeneration(Generation generation);
        Task<Generation> UpdateGeneration(Generation updatedGeneration);
    }
}
