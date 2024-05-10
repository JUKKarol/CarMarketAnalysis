using CarMarketAnalysis.Enums;

namespace CarMarketAnalysis.DTOs.CarDTOs
{
    public class CarDetailsDto
    {
        public string Name { get; set; }
        public int Price { get; set; }
        public Currency Currnecy { get; set; }
        public BodyType BodyType { get; set; }
        public int YearOfProduction { get; set; }
        public int Mileage { get; set; }
        public int EngineSize { get; set; }
        public int HorsePower { get; set; }
        public bool AutomaticTransmission { get; set; }
        public FuelType FuelType { get; set; }
        public string Localization { get; set; }
        public string Slug { get; set; }
        public bool ElectricSeat { get; set; }
        public bool HeatedSeats { get; set; }
        public bool HeatedBackSeats { get; set; }
        public bool MassagedSeats { get; set; }
        public bool FullElectricWindows { get; set; }
        public bool Bluetooth { get; set; }
        public bool CruiseControl { get; set; }
        public bool Parktronic { get; set; }
        public bool MultifunctionWheel { get; set; }
        public bool HeatedWheel { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Model { get; set; }
        public string Brand { get; set; }
    }
}