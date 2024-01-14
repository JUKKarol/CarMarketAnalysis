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
