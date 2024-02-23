using CarMarketAnalysis.DTOs.CarDTOs;
using CarMarketAnalysis.DTOs.ModelDTOs;

namespace CarMarketAnalysis.Services.ScrapServices.PlaywrightService
{
    public interface IPlaywrightService
    {
        Task<List<string>> RefreshBrands();
        Task<List<ModelDisplayDto>> RefreshModels(bool refreshForEmptyBrandsOnly);
        Task<int> GetPagesCount(string url);
        Task<CarScrapedDTO> ScrapSingleOffer(string offerUrl);
        Task<List<CarDisplayDto>> ScrapSinglePage(string pageUrl);
        Task<List<CarDisplayDto>> ScrapAllPages(string firstPageUrl);
    }
}
