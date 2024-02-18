using CarMarketAnalysis.DTOs.CarDTOs;
using CarMarketAnalysis.DTOs.ModelDTOs;

namespace CarMarketAnalysis.Services.ScrapServices.PlaywrightService
{
    public interface IPlaywrightService
    {
        Task<List<string>> RefreshBrands();
        Task<List<ModelDisplayDto>> RefreshModels(bool refreshForEmptyBrandsOnly);
        Task<int> GetPagesCount(string url);
        Task<CarCreateDto> ScrapSingleOffer(string offerUrl);
        Task<List<CarDisplayDto>> ScrapSinglePage(string pageUrl);
        Task<List<CarCreateDto>> ScrapAllPages(string firstPageUrl);
    }
}
