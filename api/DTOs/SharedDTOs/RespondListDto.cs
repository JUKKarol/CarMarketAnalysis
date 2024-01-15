namespace CarMarketAnalysis.DTOs.SharedDTOs
{
    public class RespondListDto<T>
    {
        public int PagesCount { get; set; }
        public int ItemsCount { get; set; }
        public List<T> Items { get; set; }
    }
}