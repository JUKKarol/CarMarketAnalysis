using CarMarketAnalysis.Entities;
using Sieve.Models;

namespace CarMarketAnalysis.Data.Repositories.BrandRepository
{
    public interface IBrandRepository
    {
        Task<Brand> GetBrandById(Guid brandId);

        Task<Brand> GetBrandByName(string brandName);

        Task<List<Brand>> GetBrands(SieveModel query);

        Task<List<Brand>> GetAllBrands();

        Task<List<Brand>> GetAllBrandsIncludeModels();

        Task<int> GetBrandsCount(SieveModel query);

        Task<Brand> CreateBrand(Brand brand);

        Task<List<Brand>> CreateBrands(List<Brand> brands);

        Task<Brand> UpdateBrand(Brand updatedBrand);
    }
}