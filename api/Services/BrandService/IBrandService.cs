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
        Task<BrandDisplayDto> CreateBrand(BrandCreateDto brandDto);
        Task<BrandDisplayDto> UpdateBrand(BrandUpdateDto brandDto);
    }
}
