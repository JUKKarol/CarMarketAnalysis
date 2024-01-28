using CarMarketAnalysis.Data.Repositories.BrandRepository;
using CarMarketAnalysis.DTOs.BrandDTOs;
using CarMarketAnalysis.DTOs.CarDTOs;
using CarMarketAnalysis.DTOs.ModelDTOs;
using CarMarketAnalysis.Enums;
using CarMarketAnalysis.Migrations;
using CarMarketAnalysis.Services.BrandService;
using CarMarketAnalysis.Services.ModelService;
using CarMarketAnalysis.Services.PlaywrightServices.Pages;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Routing;
using Microsoft.Playwright;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public async Task<List<ModelDisplayDto>> RefreshModels(bool refreshForEmptyBrandsOnly)
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();

            await page.GotoAsync(pages.Url);
            await page.Locator(pages.AcceptCookiesBtn).ClickAsync();

            var currnetBrands = await brandService.GetAllBrandsWithModels();

            if (refreshForEmptyBrandsOnly)
            {
                currnetBrands = currnetBrands.Where(b => b.Models.Count == 0).ToList();
            }

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

                if (modelsToInsertDto.Count != 0)
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

        public async Task<CarCreateDto> ScrapSingleOffer(string offerUrl)
        {
            using var playwright = await Playwright.CreateAsync();
            var browser = await playwright.Chromium.LaunchAsync();
            var page = await browser.NewPageAsync();

            await page.GotoAsync(offerUrl);
            await page.Locator(pages.AcceptCookiesBtn).ClickAsync();

            CarCreateDto carCreateDto = new();

            if (await page.Locator("div[data-testid='content-description-section'] > div > div > button").IsVisibleAsync())
            {
                await page.Locator("div[data-testid='content-description-section'] > div > div > button").ClickAsync();
            }

            string descriptionString = await page.Locator("div[data-testid='content-description-section']").InnerTextAsync();
            carCreateDto.Name = $"{descriptionString.Replace("\n", " ")} {ExtractDescriptionFromUrl(offerUrl)}";

            string priceString = await page.Locator("h3[class*='offer-price__number']").First.InnerTextAsync();
            carCreateDto.Price = int.Parse(priceString.Replace(" ", ""));

            string currencyString = await page.Locator("p[class*='offer-price__currency']").First.InnerTextAsync();
            carCreateDto.Currnecy = Enum.Parse<Currnecy>(currencyString);

            var detalisInfoDivs = await page.Locator("div[data-testid='advert-details-item']").AllAsync();
            var detalisInfoDivsString = await Task.WhenAll(detalisInfoDivs.Select(async d => await d.TextContentAsync()));

            string brandDiv = detalisInfoDivsString.FirstOrDefault(d => d.Contains("Marka pojazdu"));
            string brandString = brandDiv.Replace("Marka pojazdu", "");

            string modelDiv = detalisInfoDivsString.FirstOrDefault(d => d.Contains("Model pojazdu"));
            string modelString = modelDiv.Replace("Model pojazdu", "");

            var model = await modelService.GetModelByNameAndBrandName(modelString, brandString);
            carCreateDto.ModelId = model.Id;

            string fuelTypeDiv = detalisInfoDivsString.FirstOrDefault(d => d.Contains("Rodzaj paliwa"));
            string fuelTypeString = fuelTypeDiv.Replace("Rodzaj paliwa", "").Trim();

            var fuelTypeMapping = new Dictionary<string, FuelType>
            {
                { "Benzyna", FuelType.Gasoline },
                { "Benzyna+LPG", FuelType.GasolineLPG },
                { "Benzyna+CNG", FuelType.GasolineCNG },
                { "Diesel", FuelType.Diesel },
                { "Elektryczny", FuelType.Electric },
                { "Etanol", FuelType.Ethanol },
                { "Hybryda", FuelType.Hybrid },
                { "Wodór", FuelType.Hydrogen },
            };

            if (fuelTypeMapping.TryGetValue(fuelTypeString, out FuelType fuelType))
            {
                carCreateDto.FuelType = fuelType;
            }

            string bodyTypeDiv = detalisInfoDivsString.FirstOrDefault(d => d.Contains("Typ nadwozia"));
            string bodyTypeString = bodyTypeDiv.Replace("Typ nadwozia", "").Trim();

            var bodyTypeMapping = new Dictionary<string, BodyType>
            {
                { "Auta małe", BodyType.SmallCar },
                { "Auta miejskie", BodyType.UrbanCar },
                { "Coupe", BodyType.Coupe },
                { "Kabriolet", BodyType.Cabriolet },
                { "Kombi", BodyType.Combi },
                { "Kompakt", BodyType.Compact },
                { "Minivan", BodyType.Minivan },
                { "Sedan", BodyType.Sedan },
                { "SUV", BodyType.SUV },
            };

            if (bodyTypeMapping.TryGetValue(bodyTypeString, out BodyType bodyType))
            {
                carCreateDto.BodyType = bodyType;
            }

            string productionYearDiv = detalisInfoDivsString.FirstOrDefault(d => d.Contains("Rok produkcji"));
            carCreateDto.YearOfProduction = int.Parse(productionYearDiv.Replace("Rok produkcji", ""));

            string mileageDiv = detalisInfoDivsString.FirstOrDefault(d => d.Contains("Przebieg"));
            carCreateDto.Mileage = int.Parse(Regex.Replace(mileageDiv, "[^0-9]", ""));

            string engineSizeDiv = detalisInfoDivsString.FirstOrDefault(d => d.Contains("Pojemność skokowa"));
            carCreateDto.EngineSize = int.Parse(Regex.Replace(engineSizeDiv.Substring(0, engineSizeDiv.Length - 1), "[^0-9]", ""));

            string horsePowerDiv = detalisInfoDivsString.FirstOrDefault(d => d.Contains("Moc"));
            carCreateDto.HorsePower = int.Parse(Regex.Replace(horsePowerDiv, "[^0-9]", ""));

            string transmissionDiv = detalisInfoDivsString.FirstOrDefault(d => d.Contains("Skrzynia biegów"));
            carCreateDto.AutomaticTransmission = (transmissionDiv.Substring(15) == "Automatyczna");

            carCreateDto.Localization = await page.Locator("div[data-testid='aside-seller-info'] a[href='#map']").InnerTextAsync();

            carCreateDto.Slug = offerUrl.Substring(38);


            var equipmentInfoDivs = await page.Locator("div[data-testid='accordion-collapse-inner-content'] > div").AllInnerTextsAsync();

            var equipmentMapping = new Dictionary<string, Action<CarCreateDto>>()
            {
                { "Elektrycznie ustawiany fotel", dto => dto.ElectricSeat = true },
                { "Podgrzewany fotel", dto => dto.HeatedSeats = true },
                { "Ogrzewane siedzenia tylne", dto => dto.HeatedBackSeats = true },
                { "masaż", dto => dto.MassagedSeats = true },
                { "Elektryczne szyby tylne", dto => dto.FullElectricWindows = true },
                { "Bluetooth", dto => dto.Bluetooth = true },
                { "Kontrola trakcji", dto => dto.CruiseControl = true },
                { "park", dto => dto.Parktronic = true },
                { "Kierownica wielofunkcyjna", dto => dto.MultifunctionWheel = true },
                { "Kierownica ogrzewana", dto => dto.HeatedWheel = true },
            };

            foreach (var mapping in equipmentMapping)
            {
                if (equipmentInfoDivs.Any(d => d.Contains(mapping.Key)))
                {
                    mapping.Value(carCreateDto);
                }
            }


            return carCreateDto;
        }

        static string ExtractDescriptionFromUrl(string offerUrl)
        {
            string trimmedUrl = offerUrl.Substring(38);

            int index = trimmedUrl.IndexOf("-ID");
            if (index != -1)
            {
                trimmedUrl = trimmedUrl.Substring(0, index);
            }

            string result = trimmedUrl.Replace("-", " ");

            return result;
        }
    }
}
