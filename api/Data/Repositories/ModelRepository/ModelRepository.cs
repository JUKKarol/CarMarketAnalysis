﻿using CarMarketAnalysis.Entities;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace CarMarketAnalysis.Data.Repositories.ModelRepository
{
    public class ModelRepository(
        DatabaseContext db,
        ISieveProcessor sieveProcessor) : IModelRepository
    {
        public async Task<Model> GetModelById(Guid modelId)
        {
            return await db.Models
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == modelId);
        }

        public async Task<Model> GetModelByNameAndBrandId(Guid brandId, string modelName)
        {
            return await db.Models
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Name == modelName && m.BrandId == brandId);
        }

        public async Task<List<Model>> GetModels(SieveModel query)
        {
            var models = db
                .Models
                .AsNoTracking()
                .AsQueryable();

            return await sieveProcessor
                .Apply(query, models)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetModelsCount(SieveModel query)
        {
            var models = db
                .Models
                .AsNoTracking()
                .AsQueryable();

            return await sieveProcessor
                .Apply(query, models, applyPagination: false)
                .CountAsync();
        }

        public async Task<Model> CreateModel(Model model)
        {
            await db.AddAsync(model);
            await db.SaveChangesAsync();

            return model;
        }

        public async Task<List<Model>> CreateModels(List<Model> models)
        {
            await db.AddRangeAsync(models);
            await db.SaveChangesAsync();

            return models;
        }

        public async Task<Model> UpdateModel(Model updatedModel)
        {
            var model = await db.Models
                .FirstOrDefaultAsync(m => m.Id == updatedModel.Id);

            var entry = db.Entry(model);
            entry.CurrentValues.SetValues(updatedModel);

            await db.SaveChangesAsync();

            return model;
        }
    }
}