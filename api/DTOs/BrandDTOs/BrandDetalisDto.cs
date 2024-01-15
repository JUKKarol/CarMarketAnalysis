using CarMarketAnalysis.DTOs.ModelDTOs;

namespace CarMarketAnalysis.DTOs.BrandDTOs
{
    public class BrandDetalisDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<ModelDisplayDto> Models { get; set; }
    }
}
