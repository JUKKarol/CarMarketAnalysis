using CarMarketAnalysis.DTOs.ModelDTOs;

namespace CarMarketAnalysis.Services.PlaywrightServices.PlaywrightService
{
    public interface IPlaywrightService
    {
        Task<List<string>> RefreshBrands();
        Task<List<ModelDisplayDto>> RefreshModels();
        Task<int> GetPagesCount();
    }
}
