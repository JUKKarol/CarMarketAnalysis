using CarMarketAnalysis.Data.Repositories.BrandRepository;
using CarMarketAnalysis.DTOs.BrandDTOs;
using CarMarketAnalysis.DTOs.ModelDTOs;
using CarMarketAnalysis.Services.BrandService;
using CarMarketAnalysis.Services.ModelService;
using CarMarketAnalysis.Services.PlaywrightServices.Pages;
using Microsoft.AspNetCore.Routing;
using Microsoft.Playwright;
using System.Linq;
using System.Text.RegularExpressions;

namespace CarMarketAnalysis.Services.PlaywrightServices.PlaywrightService
{
    public class PlaywrightService(
        IBrandService brandService,
        IModelService modelService,
        IPages pages) : IPlaywrightService
    {
        private async Task<IPage> CreatePage()
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();

            await page.GotoAsync(pages.Url);
            await page.Locator(pages.AcceptCookiesBtn).ClickAsync();

            return page;
        }

        public async Task<List<string>> RefreshBrands()
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();

            await page.GotoAsync(pages.Url);
            await page.Locator(pages.AcceptCookiesBtn).ClickAsync();

            await page.Locator(pages.FilterBrandDiv).First.ClickAsync();
            var brandsUl = await page.Locator($"{pages.FilterBrandDiv} ul li").AllTextContentsAsync();

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

        public async Task<List<ModelDisplayDto>> RefreshModels()
        {
            var launchOptions = new BrowserTypeLaunchOptions
            {
                Headless = false
            };

            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync(launchOptions);
            var page = await browser.NewPageAsync();

            await page.GotoAsync(pages.Url);
            await page.Locator(pages.AcceptCookiesBtn).ClickAsync();

            var currnetBrands = await brandService.GetAllBrands();

            List<ModelDisplayDto> allCreatedModels = new();

            foreach (var brand in currnetBrands)
            {
                await page.Locator(pages.FilterBrandDiv).First.ClickAsync();
                await page.Locator($"{pages.FilterBrandDiv} ul").GetByText(brand.Name).First.ClickAsync();
                await page.Locator($"{pages.FilterBrandDiv} {pages.ArrowBtn}").ClickAsync();
                
                await page.Locator($"{pages.ResultHeaderDiv} ul li").GetByText(brand.Name).WaitForAsync();
                await page.Locator(pages.FilterModelDiv).First.ClickAsync();
                var modelsUl = await page.Locator($"{pages.FilterModelDiv} ul li").AllTextContentsAsync();

                var models = modelsUl.Skip(1).ToList();
                var modelsTrimmed = models.Select(model =>
                {
                    return Regex.Replace(model, @"\(.+$", "").ToLower().Trim();
                }).ToList();

                var currentModels = brand.Models.Select(m => m.Name);
                var modelsToInsert = modelsTrimmed.Except(currentModels).ToList();
                List<ModelCreateDto> modelsToInsertDto = modelsToInsert.Select(name => new ModelCreateDto { Name = name, brandId = brand.Id }).ToList();

                if (modelsToInsertDto.Any())
                {
                    var createdModels = await modelService.CreateModels(modelsToInsertDto);

                    allCreatedModels.AddRange(createdModels);
                }

                await page.Locator($"{pages.FilterModelDiv} {pages.ArrowBtn}").ClickAsync();
                await page.Locator($"{pages.FilterBrandDiv} {pages.ArrowBtn}").ClickAsync();
                await page.Locator($"{pages.ResultHeaderDiv} ul li").GetByText(brand.Name).WaitForAsync(new LocatorWaitForOptions() { State = WaitForSelectorState.Hidden });
            }

            return allCreatedModels;
        }

        public async Task<int> GetPagesCount()
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();

            await page.GotoAsync(pages.Url);
            await page.Locator(pages.AcceptCookiesBtn).ClickAsync();

            var pagesCountString = await page.Locator("ul[class*='pagination-list'] li[data-testid='pagination-list-item']").Last.InnerTextAsync();
            bool isPagesCountInt = int.TryParse(pagesCountString, out int pagesCount);

            return isPagesCountInt ? pagesCount : 0;
        }
    }
}
