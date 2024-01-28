namespace CarMarketAnalysis.Services.ScrapServices.Pages
{
    public class Pages : IPages
    {
        public string Url => "https://www.otomoto.pl/osobowe";
        public string AcceptCookiesBtn => "button[id='onetrust-accept-btn-handler']";
        public string FilterBrandDiv => "div[data-testid='filter_enum_make']";
        public string FilterModelDiv => "div[data-testid='filter_enum_model']";
        public string ArrowBtn => "button[data-testid='arrow']";
        public string ResultHeaderDiv => "div[aria-label='Results header']";
        public string DetailsItemDiv => "div[data-testid='advert-details-item']";
    }
}
