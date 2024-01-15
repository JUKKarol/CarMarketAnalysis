using CarMarketAnalysis.Services.BrandService;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace CarMarketAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController(IBrandService brandService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetBrands([FromQuery] SieveModel query)
        {
            return Ok(await brandService.GetBrands(query));
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
    }
}
