using CarMarketAnalysis.DTOs.CarDTOs;
using CarMarketAnalysis.DTOs.ModelDTOs;

namespace CarMarketAnalysis.Services.ScrapServices.PlaywrightService
{
    public interface IPlaywrightService
    {
        Task<List<string>> RefreshBrands();
        Task<List<ModelDisplayDto>> RefreshModels(bool refreshForEmptyBrandsOnly);
        Task<int> GetPagesCount();
        Task<CarCreateDto> ScrapSingleOffer(string offerUrl);
        Task<List<CarCreateDto>> ScrapSinglePage(string pageUrl);
    }
}
