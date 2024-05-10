using CarMarketAnalysis.Services.ModelService;
using CarMarketAnalysis.Services.ScrapServices.PlaywrightService;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace CarMarketAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController(IPlaywrightService playwrightService) : ControllerBase
    {
        [HttpGet("offer")]
        public async Task<IActionResult> ScrapSingleOffer(string url)
        {
            return Ok(await playwrightService.ScrapSingleOffer(url));
        }

        [HttpPut("page")]
        public async Task<IActionResult> ScrapSinglePage(string url)
        {
            return Ok(await playwrightService.ScrapSinglePage(url));
        }

        [HttpPut("all")]
        public async Task<IActionResult> ScrapAllPages(string url)
        {
            return Ok(await playwrightService.ScrapAllPages(url));
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAllCars(string url)
        {
            return Ok(await playwrightService.ScrapAllPages(url));
        }
    }
}