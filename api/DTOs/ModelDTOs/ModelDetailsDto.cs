namespace CarMarketAnalysis.DTOs.ModelDTOs
{
    public class ModelDetailsDto
    {
        public Guid Id { get; set; }
        public Guid brandId { get; set; }
        public string Name { get; set; }
        public string BrandName { get; set; }

        public List<GenerationDisplayDto> Generations { get; set; }
    }
}