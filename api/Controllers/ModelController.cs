using CarMarketAnalysis.Services.BrandService;
using CarMarketAnalysis.Services.ModelService;
using CarMarketAnalysis.Services.ScrapServices.PlaywrightService;
using Microsoft.AspNetCore.Mvc;
using Sieve.Models;

namespace CarMarketAnalysis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ModelController(
        IModelService modelService, 
        IPlaywrightService playwrightService) : ControllerBase

    {
        [HttpGet]
        public async Task<IActionResult> GetModels([FromQuery] SieveModel query)
        {
            return Ok(await modelService.GetModels(query));
        }

        [HttpGet("{modelId}")]
        public async Task<IActionResult> GetModel(Guid modelId)
        {
            var model = await modelService.GetModelById(modelId);

            if (model == null)
            {
                return NotFound("Model not found");
            }

            return Ok(model);
        }

        [HttpPut]
        public async Task<IActionResult> RefreshModels()
        {
            return Ok(await playwrightService.RefreshModels(false));
        }

        [HttpPut("emptyBrandsOnly")]
        public async Task<IActionResult> RefreshModelsForEmptyBrandsOnly()
        {
            return Ok(await playwrightService.RefreshModels(true));
        }
    }
}
