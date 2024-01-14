using CarMarketAnalysis.Entities;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace CarMarketAnalysis.Data.Repositories.GenerationRepository
{
    public class GenerationRepository(
        DatabaseContext db,
        ISieveProcessor sieveProcessor) : IGenerationRepository
    {
        public async Task<Generation> GetGenerationById(Guid generationId)
        {
            return await db.Generations
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == generationId);
        }

        public async Task<List<Generation>> GetGenerations(SieveModel query)
        {
            var generations = db
                .Generations
                .AsNoTracking()
                .AsQueryable();

            return await sieveProcessor
                .Apply(query, generations)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetGenerationsCount(SieveModel query)
        {
            var generations = db
                .Generations
                .AsNoTracking()
                .AsQueryable();

            return await sieveProcessor
                .Apply(query, generations, applyPagination: false)
                .CountAsync();
        }

        public async Task<Generation> CreateGeneration(Generation generation)
        {
            await db.AddAsync(generation);
            await db.SaveChangesAsync();

            return generation;
        }

        public async Task<Generation> UpdateGeneration(Generation updatedGeneration)
        {
            var generation = await db.Generations
                .FirstOrDefaultAsync(g => g.Id == updatedGeneration.Id);

            var entry = db.Entry(generation);
            entry.CurrentValues.SetValues(updatedGeneration);

            await db.SaveChangesAsync();

            return generation;
        }
    }
}
