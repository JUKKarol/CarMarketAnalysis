using CarMarketAnalysis.Entities;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System.Net;

namespace CarMarketAnalysis.Data.Repositories.BrandRepository
{
    public class BrandRepository(
        DatabaseContext db,
        ISieveProcessor sieveProcessor) : IBrandRepository
    {
        public async Task<Brand> GetBrandById(Guid brandId)
        {
            return await db.Brands
                .AsNoTracking()
                .FirstOrDefaultAsync(g => g.Id == brandId);
        }

        public async Task<List<Brand>> GetBrands(SieveModel query)
        {
            var brands = db
                .Brands
                .AsNoTracking()
                .AsQueryable();

            return await sieveProcessor
                .Apply(query, brands)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetBrandsCount(SieveModel query)
        {
            var brands = db
                .Brands
                .AsNoTracking()
                .AsQueryable();

            return await sieveProcessor
                .Apply(query, brands, applyPagination: false)
                .CountAsync();
        }

        public async Task<Brand> CreateBrand(Brand brand)
        {
            await db.AddAsync(brand);
            await db.SaveChangesAsync();

            return brand;
        }

        public async Task<Brand> UpdateBrand(Brand updatedBrand)
        {
            var brand = await db.Brands
                .FirstOrDefaultAsync(c => c.Id == updatedBrand.Id);

            var entry = db.Entry(brand);
            entry.CurrentValues.SetValues(updatedBrand);

            await db.SaveChangesAsync();

            return brand;
        }
    }
}
