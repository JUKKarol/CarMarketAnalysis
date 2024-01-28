using CarMarketAnalysis.DTOs.ModelDTOs;
using CarMarketAnalysis.Enums;

namespace CarMarketAnalysis.DTOs.CarDTOs
{
    public class CarDisplayDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public Currency Currnecy { get; set; }
        public BodyType BodyType { get; set; }
        public int YearOfProduction { get; set; }
        public int Mileage { get; set; }
        public int EngineSize { get; set; }
        public FuelType FuelType { get; set; }
        public string Localization { get; set; }
        public string Slug { get; set; }

        public DateTime CreatedAt { get; set; }

        public ModelDisplayDto Model { get; set; }
    }
}
