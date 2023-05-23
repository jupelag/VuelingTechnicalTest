using GNB.SalesEnquiry.ApiProvider.Contracts;

namespace GNB.SalesEnquiry.ApiProvider
{
    public abstract class ApiProviderBase
    {
        protected readonly IApiProviderHttpClient _client;
        protected readonly IApiProviderConfig _config;

        protected ApiProviderBase(IApiProviderHttpClient client, IApiProviderConfig config)
        {
            _client = client;
            _config = config;
        }

        public async Task<T[]> GetAsync<T>(string url)
        {
            var response = await _client.GetAsync(url);
            response.EnsureSuccessStatusCode();
            try
            {
                var rates = (await response.DeserializeAsync<List<T>>()) ?? new List<T>();
                return rates.ToArray();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deserializing response: {ex.Message}", ex);
            }
        }
    }
}
