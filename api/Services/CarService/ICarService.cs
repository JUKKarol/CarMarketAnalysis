using CarMarketAnalysis.DTOs.CarDTOs;

namespace CarMarketAnalysis.Services.CarService
{
    public interface ICarService
    {
        Task<List<CarDetailsDto>> GetCars();

        Task<List<CarDisplayDto>> CreateCars(List<CarCreateDto> carsDto);
    }
}