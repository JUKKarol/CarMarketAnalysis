using AutoMapper;
using CarMarketAnalysis.Data.Repositories.BrandRepository;
using CarMarketAnalysis.DTOs.BrandDTOs;
using CarMarketAnalysis.DTOs.SharedDTOs;
using CarMarketAnalysis.Entities;
using Sieve.Models;

namespace CarMarketAnalysis.Services.BrandService
{
    public class BrandService(
        IBrandRepository brandRepository,
        IMapper mapper) : IBrandService
    {
        public async Task<BrandDetailsDto> GetBrandById(Guid brandId)
        {
            var brand = await brandRepository.GetBrandById(brandId);
            return mapper.Map<BrandDetailsDto>(brand);
        }

        public async Task<BrandDetailsDto> GetBrandByName(string brandName)
        {
            var brand = await brandRepository.GetBrandByName(brandName);
            return mapper.Map<BrandDetailsDto>(brand);
        }

        public async Task<RespondListDto<BrandDisplayDto>> GetBrands(SieveModel query)
        {
            int pageSize = query.PageSize != null ? (int)query.PageSize : 40;

            var brands = await brandRepository.GetBrands(query);
            var brandsDto = mapper.Map<List<BrandDisplayDto>>(brands);

            RespondListDto<BrandDisplayDto> respondListDto = new();
            respondListDto.Items = brandsDto;
            respondListDto.ItemsCount = await brandRepository.GetBrandsCount(query);
            respondListDto.PagesCount = (int)Math.Ceiling((double)respondListDto.ItemsCount / pageSize);

            return respondListDto;
        }

        public async Task<BrandDisplayDto> CreateBrand(BrandCreateDto brandDto)
        {
            var brand = mapper.Map<Brand>(brandDto);
            await brandRepository.CreateBrand(brand);

            return mapper.Map<BrandDisplayDto>(brand);
        }

        public async Task<BrandDisplayDto> UpdateBrand(BrandUpdateDto brandDto)
        {
            var brand = mapper.Map<Brand>(brandDto);
            await brandRepository.UpdateBrand(brand);

            return mapper.Map<BrandDisplayDto>(brand);
        }
    }
}
