using CarMarketAnalysis.Configuration.Options;

namespace CarMarketAnalysis.Configuration
{
    public class AppSettings
    {
        public ConnectionStringsOptions ConnectionStrings { get; set; }
        public SieveOptions Sieve { get; set; }
    }
}
