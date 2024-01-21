namespace CarMarketAnalysis.Services.PlaywrightServices.Pages
{
    public class Pages : IPages
    {
        public string Url => "https://www.otomoto.pl/osobowe";
        public string AcceptCookiesBtn => "button[id='onetrust-accept-btn-handler']";
    }
}
