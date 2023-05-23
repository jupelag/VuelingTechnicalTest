namespace GNB.SalesEnquiry.ApiProvider.Contracts
{
    public interface IApiProviderConfig
    {
        string RatesEndPoint { get; set; }
        string TransactionsEndPoint { get; set; }
    }
}
