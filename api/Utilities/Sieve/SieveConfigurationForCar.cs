using CarMarketAnalysis.Entities;
using Sieve.Services;

namespace CarMarketAnalysis.Utilities.Sieve
{
    public class SieveConfigurationForCar : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Car>(c => c.Name)
                .CanFilter()
                .CanSort();

            mapper.Property<Car>(b => b.Id)
               .CanFilter()
               .CanSort();
        }
    }
}
