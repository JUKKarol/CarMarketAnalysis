namespace CarMarketAnalysis.Services.PlaywrightServices.Pages
{
    public interface IPages
    {
        string Url { get; }
        string AcceptCookiesBtn { get; }
        string FilterBrandDiv { get; }
        string FilterModelDiv { get; }
        string ArrowBtn { get; }
        string ResultHeaderDiv { get; }
        string DetailsItemDiv { get; }
    }
}
