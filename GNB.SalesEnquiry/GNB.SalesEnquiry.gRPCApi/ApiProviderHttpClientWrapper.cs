using GNB.SalesEnquiry.ApiProvider.Contracts;

namespace GNB.SalesEnquiry.gRPCApi
{
    public class CurrencyConverterHttpClient:IApiProviderHttpClient
    {
        private readonly HttpClient _httpClient;
        public CurrencyConverterHttpClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<HttpResponseMessage> GetAsync(string url) => await _httpClient.GetAsync(url);
    }
}
