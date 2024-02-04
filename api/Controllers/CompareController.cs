using CarMarketAnalysis.Services.ScrapServices.PlaywrightService;
using Microsoft.AspNetCore.Mvc;

namespace CarMarketAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompareController(IPlaywrightService playwrightService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetOffer(string firstUrl, string secondUrl)
        {
            return Ok(await playwrightService.CompareOffers(firstUrl, secondUrl));
        }
    }
}
