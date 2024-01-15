using CarMarketAnalysis.DTOs.BrandDTOs;
using CarMarketAnalysis.DTOs.SharedDTOs;
using Sieve.Models;

namespace CarMarketAnalysis.Services.BrandService
{
    public interface IBrandService
    {
        Task<BrandDetalisDto> GetBrandById(Guid brandId);
        Task<BrandDetalisDto> GetBrandByName(string brandName);
        Task<RespondListDto<BrandDisplayDto>> GetBrands(SieveModel query);
        Task<BrandDisplayDto> CreateBrand(BrandCreateDto brandDto);
        Task<BrandDisplayDto> UpdateBrand(BrandUpdateDto brandDto);
    }
}
