using CarMarketAnalysis.Entities;
using Sieve.Services;

namespace CarMarketAnalysis.Utilities.Sieve
{
    public class SieveConfigurationForBrand : ISieveConfiguration
    {
        void ISieveConfiguration.Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Brand>(b => b.Name)
                .CanFilter()
                .CanSort();

            mapper.Property<Brand>(b => b.Id)
                .CanFilter()
                .CanSort();
        }
    }
}