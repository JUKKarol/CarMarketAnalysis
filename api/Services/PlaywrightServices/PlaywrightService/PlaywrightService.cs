using CarMarketAnalysis.Data.Repositories.BrandRepository;
using CarMarketAnalysis.DTOs.BrandDTOs;
using CarMarketAnalysis.Services.BrandService;
using CarMarketAnalysis.Services.PlaywrightServices.Pages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace CarMarketAnalysis.Services.PlaywrightServices.PlaywrightService
{
    public class PlaywrightService(
        IBrandService brandService,
        IPages pages) : IPlaywrightService
    {
        public async Task<List<string>> RefreshBrands()
        {
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync();

            var page = await browser.NewPageAsync();
            await page.GotoAsync(pages.Url);

            await page.Locator(pages.AcceptCookiesBtn).ClickAsync();

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
        }

        public async Task<int> GetPagesCount()
        {
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync();

            var page = await browser.NewPageAsync();
            await page.GotoAsync(pages.Url);

            await page.Locator(pages.AcceptCookiesBtn).ClickAsync();

            var pagesCountString = await page.Locator("ul[class*='pagination-list'] li[data-testid='pagination-list-item']").Last.InnerTextAsync();
            bool isPagesCountInt = int.TryParse(pagesCountString, out int pagesCount);

            return isPagesCountInt ? pagesCount : 0;
        }
    }
}
