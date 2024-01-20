using Microsoft.AspNetCore.Routing;
using Microsoft.Playwright;
using System.Text.RegularExpressions;

namespace CarMarketAnalysis.Services.PlaywrightServices.PlaywrightService
{
    public class PlaywrightService : IPlaywrightService
    {
        public async Task<string> GetPageCount()
        {
            using var playwright = await Playwright.CreateAsync();

            await using var browser = await playwright.Chromium.LaunchAsync();

            var page = await browser.NewPageAsync();
            await page.GotoAsync("https://www.otomoto.pl/osobowe");

            return await page.Locator("ul[class*='pagination-list'] li[data-testid='pagination-list-item']").Last.InnerTextAsync();
        }
    }
}
