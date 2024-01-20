using Bogus;
using CarMarketAnalysis.Entities;
using CarMarketAnalysis.Enums;
using System;

namespace CarMarketAnalysis.Data.Seeders
{
    public class Seeder(DatabaseContext db)
    {
        public void Seed(int recordsToSeed)
        {
            if (!db.Models.Any() && !db.Brands.Any() && !db.Cars.Any())
            {

                var locale = "pl";

                var brandGenerator = new Faker<Brand>()
                        .RuleFor(b => b.Id, f => f.Random.Guid())
                        .RuleFor(b => b.Name, f => f.Vehicle.Manufacturer());

                var modelGenerator = new Faker<Model>()
                        .RuleFor(m => m.Id, f => f.Random.Guid())
                        .RuleFor(m => m.Name, f => f.Vehicle.Model());

                var carGenerator = new Faker<Car>(locale)
                        .RuleFor(c => c.Id, f => f.Random.Guid())
                        .RuleFor(c => c.Name, f => f.Lorem.Sentence(10, 10))
                        .RuleFor(c => c.Slug, f => f.Lorem.Sentence(1))
                        .RuleFor(c => c.Price, f => f.Random.Int(1000, 1000000))
                        .RuleFor(c => c.HorsePower, f => f.Random.Int(50, 400))
                        .RuleFor(c => c.AutomaticTransmission, f => f.Random.Bool())
                        .RuleFor(c => c.ElectricSeat, f => f.Random.Bool())
                        .RuleFor(c => c.HeatedSeats, f => f.Random.Bool())
                        .RuleFor(c => c.HeatedBackSeats, f => f.Random.Bool())
                        .RuleFor(c => c.MassagedSeats, f => f.Random.Bool())
                        .RuleFor(c => c.FullElectricWindows, f => f.Random.Bool())
                        .RuleFor(c => c.Bluetooth, f => f.Random.Bool())
                        .RuleFor(c => c.CruiseControl, f => f.Random.Bool())
                        .RuleFor(c => c.Parktronic, f => f.Random.Bool())
                        .RuleFor(c => c.MultifunctionWheel, f => f.Random.Bool())
                        .RuleFor(c => c.HeatedWheel, f => f.Random.Bool())
                        .RuleFor(c => c.Mileage, f => f.Random.Int(10000, 500000))
                        .RuleFor(c => c.BodyType, f => f.Random.Enum<BodyType>())
                        .RuleFor(c => c.FuelType, f => f.Random.Enum<FuelType>())
                        .RuleFor(c => c.EngineSize, f => f.Random.Int(800, 4000))
                        .RuleFor(c => c.Localization, f => f.Person.Address.City)
                        .RuleFor(c => c.YearOfProduction, f => f.Random.Int(1950, DateTime.UtcNow.Year));

                List<Brand> brands = [];
                List<Model> models = [];
                List<Car> cars = [];

                for (int i = 0; i < recordsToSeed; i++)
                {
                    var brand = brandGenerator.Generate();
                    var model = modelGenerator.Generate();
                    var car = carGenerator.Generate();


                    model.BrandId = brand.Id;
                    car.ModelId = model.Id;



                    brands.Add(brand);
                    models.Add(model);
                    cars.Add(car);
                }

                db.AddRange(brands);
                db.AddRange(models);
                db.AddRange(cars);

                db.SaveChanges();
            }
        }
    }
}
