using GNB.SalesEnquiry.ApiProvider.Contracts;

namespace GNB.SalesEnquiry.gRPCApi
{
    public class ApiProviderConfig:IApiProviderConfig
    {
        public string RatesEndPoint { get; set; }
        public string TransactionsEndPoint { get; set; }
    }
}
