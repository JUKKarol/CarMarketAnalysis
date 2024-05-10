using CarMarketAnalysis.DTOs.SharedDTOs;
using CarMarketAnalysis.DTOs.CarDTOs;
using Sieve.Models;

namespace CarMarketAnalysis.Services.CarService
{
    public interface ICarService
    {
        Task<List<CarDetailsDto>> GetCars();

        Task<List<CarDisplayDto>> CreateCars(List<CarCreateDto> carsDto);
    }
}