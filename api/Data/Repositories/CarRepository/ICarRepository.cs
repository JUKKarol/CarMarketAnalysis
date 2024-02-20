using CarMarketAnalysis.Entities;
using Sieve.Models;

namespace CarMarketAnalysis.Data.Repositories.CarRepository
{
    public interface ICarRepository
    {
        Task<Car> GetCarById(Guid carId);
        Task<List<Car>> GetCars(SieveModel query);
        Task<int> GetCarsCount(SieveModel query);
        Task<Car> CreateCar(Car car);
        Task<List<Car>> CreateCars(List<Car> cars);
        Task<List<Car>> RemoveExistingCarsCreatedWithin5Days(List<Car> cars);
        Task<Car> UpdateCar(Car updatedCar);
    }
}
