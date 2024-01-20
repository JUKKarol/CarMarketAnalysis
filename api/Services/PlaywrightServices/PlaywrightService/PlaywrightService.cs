using CarMarketAnalysis.Data.Repositories.BrandRepository;
using CarMarketAnalysis.DTOs.BrandDTOs;
using CarMarketAnalysis.Services.BrandService;
using Microsoft.AspNetCore.Routing;
using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace CarMarketAnalysis.Services.PlaywrightServices.PlaywrightService
{
    public class PlaywrightService(IBrandService brandService) : IPlaywrightService
    {
        public async Task<List<string>> RefreshBrands()
        {
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync();

            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://www.otomoto.pl/osobowe");

            await page.Locator("button[id='onetrust-accept-btn-handler']").ClickAsync();

            await page.Locator("div[data-testid='filter_enum_make']").First.ClickAsync();

            var brandsUl = await page.Locator("div[data-testid='filter_enum_make'] ul li").AllTextContentsAsync();

            var brands = brandsUl.Skip(1).ToList();

            var brandsTrimmed = brands.Select(brand =>
            {
                return Regex.Replace(brand, @"[\d\(\)]", "").ToLower().Trim();
            }).ToList();

            var currnetBrands = await brandService.GetAllBrandsAsString();

            var brandsToInsert = brandsTrimmed.Except(currnetBrands).ToList();

            List<BrandCreateDto> brandsToInsertDto = brandsToInsert.Select(name => new BrandCreateDto { Name = name }).ToList();

            await brandService.CreateBrands(brandsToInsertDto);

            return brandsToInsert;

            //return await page.Locator("ul[class*='pagination-list'] li[data-testid='pagination-list-item']").Last.InnerTextAsync();
        }
    }
}
