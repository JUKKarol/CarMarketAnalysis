using AutoMapper;
using CarMarketAnalysis.Data.Repositories.CarRepository;
using CarMarketAnalysis.DTOs.CarDTOs;
using CarMarketAnalysis.DTOs.SharedDTOs;
using CarMarketAnalysis.Entities;
using Sieve.Models;

namespace CarMarketAnalysis.Services.CarService
{
    public class CarService(
        ICarRepository carRepository,
        IMapper mapper) : ICarService
    {
        public async Task<List<CarDetailsDto>> GetCars()
        {
            var cars = await carRepository.GetCars();
            var carsDto = mapper.Map<List<CarDetailsDto>>(cars);

            return carsDto;
        }

        public async Task<List<CarDisplayDto>> CreateCars(List<CarCreateDto> carsDto)
        {
            var cars = mapper.Map<List<Car>>(carsDto);

            var carsWithoutNulls = cars.Where(c => c.ModelId != Guid.Empty).ToList();
            var carsWithoutCreatedWithin5Days = await carRepository.RemoveExistingCarsCreatedWithin5Days(carsWithoutNulls);
            var createdCars = mapper.Map<List<CarDisplayDto>>(await carRepository.CreateCars(carsWithoutCreatedWithin5Days));

            return createdCars;
        }
    }
}