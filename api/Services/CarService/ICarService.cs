﻿using CarMarketAnalysis.DTOs.SharedDTOs;
using CarMarketAnalysis.DTOs.CarDTOs;
using Sieve.Models;

namespace CarMarketAnalysis.Services.CarService
{
    public interface ICarService
    {
        Task<CarDetailsDto> GetCarById(Guid carId);
        Task<RespondListDto<CarDisplayDto>> GetCars(SieveModel query);
        Task<List<CarDisplayDto>> CreateCars(List<CarCreateDto> carsDto);
    }
}
