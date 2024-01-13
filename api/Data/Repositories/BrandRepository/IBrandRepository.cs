using CarMarketAnalysis.Entities;
using Sieve.Models;

namespace CarMarketAnalysis.Data.Repositories.BrandRepository
{
    public interface IBrandRepository
    {
        Task<Brand> GetBrandById(Guid brandId);
        Task<List<Brand>> GetBrands(SieveModel query);
        Task<int> GetBrandsCount(SieveModel query);
        Task<Brand> CreateBrand(Brand brand);
        Task<Brand> UpdateBrand(Brand updatedBrand);
    }
}
