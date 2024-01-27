using CarMarketAnalysis.Services.ModelService;
using CarMarketAnalysis.Services.PlaywrightServices.PlaywrightService;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace CarMarketAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController(IPlaywrightService playwrightService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetOffer(string url)
        {
            return Ok(await playwrightService.ScrapSingleOffer(url));
        }
    }
}
