﻿using AutoMapper;
using CarMarketAnalysis.Data.Repositories.BrandRepository;
using CarMarketAnalysis.Data.Repositories.ModelRepository;
using CarMarketAnalysis.DTOs.ModelDTOs;
using CarMarketAnalysis.DTOs.SharedDTOs;
using CarMarketAnalysis.Entities;
using Sieve.Models;

namespace CarMarketAnalysis.Services.ModelService
{
    public class ModelService(
        IModelRepository modelRepository,
        IBrandRepository brandRepository,
        IMapper mapper) : IModelService
    {
        public async Task<ModelDetailsDto> GetModelById(Guid modelId)
        {
            var model = await modelRepository.GetModelById(modelId);
            var modelDto = mapper.Map<ModelDetailsDto>(model);

            return modelDto;
        }

        public async Task<ModelDetailsDto> GetModelByNameAndBrandName(string modelName, string brandName)
        {
            var brand = await brandRepository.GetBrandByName(brandName);
            var model = await modelRepository.GetModelByNameAndBrandId(brand.Id, modelName);
            var modelDto = mapper.Map<ModelDetailsDto>(model);

            return modelDto;
        }

        public async Task<RespondListDto<ModelDisplayDto>> GetModels(SieveModel query)
        {
            int pageSize = query.PageSize != null ? (int)query.PageSize : 40;

            var models = await modelRepository.GetModels(query);
            var modelsDto = mapper.Map<List<ModelDisplayDto>>(models);

            RespondListDto<ModelDisplayDto> respondListDto = new();
            respondListDto.Items = modelsDto;
            respondListDto.ItemsCount = await modelRepository.GetModelsCount(query);
            respondListDto.PagesCount = (int)Math.Ceiling((double)respondListDto.ItemsCount / pageSize);

            return respondListDto;
        }

        public async Task<ModelDisplayDto> CreateModel(ModelCreateDto modelDto)
        {
            var model = mapper.Map<Model>(modelDto);
            await modelRepository.CreateModel(model);

            return mapper.Map<ModelDisplayDto>(model);
        }

        public async Task<List<ModelDisplayDto>> CreateModels(List<ModelCreateDto> modelsDto)
        {
            var models = mapper.Map<List<Model>>(modelsDto);
            await modelRepository.CreateModels(models);

            return mapper.Map<List<ModelDisplayDto>>(models);
        }

        public async Task<ModelDisplayDto> UpdateModel(ModelUpdateDto modelDto)
        {
            var model = mapper.Map<Model>(modelDto);
            await modelRepository.UpdateModel(model);

            return mapper.Map<ModelDisplayDto>(model);
        }
    }
}