namespace GNB.SalesEnquiry.ApiProvider.Contracts
{
    public interface IApiProviderHttpClient
    {
        Task<HttpResponseMessage> GetAsync(string url);
        
    }
}