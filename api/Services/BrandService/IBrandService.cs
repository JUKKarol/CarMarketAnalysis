using CarMarketAnalysis.DTOs.BrandDTOs;
using CarMarketAnalysis.DTOs.SharedDTOs;
using Sieve.Models;

namespace CarMarketAnalysis.Services.BrandService
{
    public interface IBrandService
    {
        Task<BrandDetailsDto> GetBrandById(Guid brandId);

        Task<BrandDetailsDto> GetBrandByName(string brandName);

        Task<RespondListDto<BrandDisplayDto>> GetBrands(SieveModel query);

        Task<List<string>> GetAllBrandsAsString();

        Task<List<BrandDisplayDto>> GetAllBrands();

        Task<List<BrandDetailsDto>> GetAllBrandsWithModels();

        Task<List<BrandDisplayDto>> CreateBrands(List<BrandCreateDto> brandsDto);

        Task<BrandDisplayDto> UpdateBrand(BrandUpdateDto brandDto);
    }
}