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
        public async Task<CarDetailsDto> GetCarById(Guid carId)
        {
            var car = await carRepository.GetCarById(carId);
            return mapper.Map<CarDetailsDto>(car);
        }

        public async Task<RespondListDto<CarDisplayDto>> GetCars(SieveModel query)
        {
            int pageSize = query.PageSize != null ? (int)query.PageSize : 40;

            var cars = await carRepository.GetCars(query);
            var carsDto = mapper.Map<List<CarDisplayDto>>(cars);

            RespondListDto<CarDisplayDto> respondListDto = new();
            respondListDto.Items = carsDto;
            respondListDto.ItemsCount = await carRepository.GetCarsCount(query);
            respondListDto.PagesCount = (int)Math.Ceiling((double)respondListDto.ItemsCount / pageSize);
            return respondListDto;
        }

        public async Task<List<CarDisplayDto>> CreateCars(List<CarCreateDto> carsDto)
        {
            var cars = mapper.Map<List<Car>>(carsDto);
            var createdCars = mapper.Map<List<CarDisplayDto>>(await carRepository.CreateCars(cars));

            return createdCars;
        }
    }
}
