namespace GNB.SalesEnquiry.ApiProvider
{
    public class CurrencyConverterHttpClient
    {
        private readonly HttpClient _httpClient;
        public CurrencyConverterHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> GetAsync(string url) => await _httpClient.GetAsync(url);
    }
}
