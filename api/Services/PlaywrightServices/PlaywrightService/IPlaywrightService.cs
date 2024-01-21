namespace CarMarketAnalysis.Services.PlaywrightServices.PlaywrightService
{
    public interface IPlaywrightService
    {
        Task<List<string>> RefreshBrands();
        Task<int> GetPagesCount();
    }
}
