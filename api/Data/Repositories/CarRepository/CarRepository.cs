using CarMarketAnalysis.Entities;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;
using System.Net;

namespace CarMarketAnalysis.Data.Repositories.CarRepository
{
    public class CarRepository(
        DatabaseContext db,
        ISieveProcessor sieveProcessor) : ICarRepository
    {
        public async Task<Car> GetCarById(Guid carId)
        {
            return await db.Cars
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Id == carId);
        }

        public async Task<List<Car>> GetCars(SieveModel query)
        {
            var cars = db
                .Cars
                .AsNoTracking()
                .AsQueryable();

            return await sieveProcessor
                .Apply(query, cars)
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<int> GetCarsCount(SieveModel query)
        {
            var cars = db
                .Cars
                .AsNoTracking()
                .AsQueryable();

            return await sieveProcessor
                .Apply(query, cars, applyPagination: false)
                .CountAsync();
        }

        public async Task<Car> CreateCar(Car car)
        {
            await db.AddAsync(car);
            await db.SaveChangesAsync();

            return car;
        }

        public async Task<List<Car>> CreateCars(List<Car> cars)
        {
            await db.AddRangeAsync(cars);
            await db.SaveChangesAsync();

            return cars;
        }

        public async Task<List<Car>> RemoveExistingCarsCreatedWithin5Days(List<Car> cars)
        {
            var existingSlugs = await db.Cars
                .Where(c => cars.Select(car => car.Slug).Contains(c.Slug))
                .ToListAsync();

            var slugsToDelete = existingSlugs
                .Where(c => (DateTime.UtcNow - c.CreatedAt).TotalDays <= 5)
                .Select(c => c.Slug)
                .ToList();

            cars.RemoveAll(car => slugsToDelete.Contains(car.Slug));

            return cars;
        }

        public async Task<Car> UpdateCar(Car updatedCar)
        {
            var car = await db.Cars
                .FirstOrDefaultAsync(c => c.Id == updatedCar.Id);

            var entry = db.Entry(car);
            entry.CurrentValues.SetValues(updatedCar);

            await db.SaveChangesAsync();

            return car;
        }
    }
}
