using CarMarketAnalysis.Enums;

namespace CarMarketAnalysis.Entities
{
    public class Car
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public int Price { get; set; }
        public Currnecy Currnecy { get; set; } = Currnecy.PLN;
        public BodyType BodyType { get; set; } = BodyType.None;
        public int YearOfProduction { get; set; }
        public int Mileage { get; set; }
        public int EngineSize { get; set; }
        public int HorsePower { get; set; }
        public bool AutomaticTransmission { get; set; }
        public FuelType FuelType { get; set; } = FuelType.None;
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

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid ModelId { get; set; }

        public Model Model { get; set; }
    }
}
