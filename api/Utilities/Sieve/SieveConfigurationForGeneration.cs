using CarMarketAnalysis.Entities;
using Sieve.Services;

namespace CarMarketAnalysis.Utilities.Sieve
{
    public class SieveConfigurationForGeneration : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Generation>(g => g.Name)
               .CanFilter()
               .CanSort();

            mapper.Property<Generation>(g => g.Id)
               .CanFilter()
               .CanSort();

            mapper.Property<Generation>(g => g.ModelId)
               .CanFilter()
               .CanSort();
        }
    }
}
