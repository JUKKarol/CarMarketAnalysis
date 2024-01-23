using CarMarketAnalysis.Services.BrandService;
using CarMarketAnalysis.Services.PlaywrightServices.PlaywrightService;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace CarMarketAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(
        IBrandService brandService,
        IPlaywrightService playwrightService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBrands()
        {
            return Ok(await brandService.GetAllBrands());
        }

        [HttpGet("{brandId}")]
        public async Task<IActionResult> GetBrand(Guid brandId)
        {
            var brand = await brandService.GetBrandById(brandId);

            if (brand == null)
            {
                return NotFound("Brand not found");
            }

            return Ok(brand);
        }

        [HttpPut]
        public async Task<IActionResult> RefreshBrands()
        {
            return Ok(await playwrightService.RefreshBrands());
        }
    }
}
