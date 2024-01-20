using CarMarketAnalysis.DTOs.GenerationDTOs;

namespace CarMarketAnalysis.DTOs.ModelDTOs
{
    public class ModelDetailsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public List<GenerationDisplayDto> Generations { get; set; }
    }
}