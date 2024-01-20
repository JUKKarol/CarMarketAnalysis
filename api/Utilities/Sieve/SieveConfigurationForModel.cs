using CarMarketAnalysis.Entities;
using Sieve.Services;

namespace CarMarketAnalysis.Utilities.Sieve
{
    public class SieveConfigurationForModel : ISieveConfiguration
    {
        public void Configure(SievePropertyMapper mapper)
        {
            mapper.Property<Model>(m => m.Name)
                .CanFilter()
                .CanSort();

            mapper.Property<Model>(m => m.Id)
               .CanFilter()
               .CanSort();

            mapper.Property<Model>(m => m.BrandId)
               .CanFilter()
               .CanSort();
        }
    }
}
