using GNB.SalesEnquiry.ApiProvider;
using GNB.SalesEnquiry.ApiProvider.Contracts;
using GNB.SalesEnquiry.Core;
using GNB.SalesEnquiry.Core.Contracts;
using GNB.SalesEnquiry.DataAccess;
using GNB.SalesEnquiry.DbProvider;
using GNB.SalesEnquiry.gRPCApi;
using GNB.SalesEnquiry.gRPCApi.Services;
using GNB.SalesEnquiry.Provider;
using GNB.SalesEnquiry.Provider.Contracts;
using GNB.SalesEnquiry.RateCompleter;
using Polly.Extensions.Http;
using Polly;
using CurrencyConverterHttpClient = GNB.SalesEnquiry.gRPCApi.CurrencyConverterHttpClient;
using SalesEnquiry = GNB.SalesEnquiry.Core.SalesEnquiry;

var builder = WebApplication.CreateBuilder(args);

var config = builder.Configuration.GetSection("ApiProviderConfig").Get<ApiProviderConfig>();
builder.Services.AddSingleton<IApiProviderConfig>(config);
builder.Services.AddLogging();
builder.Services.AddSingleton<ISalesEnquiry, SalesEnquiry>();
builder.Services.AddSingleton<IProvider, Provider>();
builder.Services.AddSingleton<ICompleter, Completer>();
builder.Services.AddSingleton<IConverter, Converter>();
builder.Services.AddSingleton<IApiRatesProvider, ApiRateProvider>();
builder.Services.AddSingleton<IApiTransactionsProvider, ApiTransactionsProvider>();
builder.Services.AddSingleton<IDbRatesProvider, DbRatesProvider>();
builder.Services.AddSingleton<IDbTransactionsProvider, DbTransactionsProvider>();
builder.Services.AddSingleton<IRatesDao, RatesDao>();
builder.Services.AddSingleton<GNB.SalesEnquiry.DbProvider.Contracts.IRatesDao, RatesDao>();
builder.Services.AddSingleton<ITransactionsDao, TransactionsDao>();
builder.Services.AddSingleton<GNB.SalesEnquiry.DbProvider.Contracts.ITransactionsDao, TransactionsDao>();
builder.Services.AddHttpClient<IApiProviderHttpClient, CurrencyConverterHttpClient>().AddPolicyHandler(GetRetryPolicy());

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapGrpcService<SalesEnquiryService>();
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}