using System.Text.Json;
using GNB.SalesEnquiry.Shared.Dtos;

namespace GNB.SalesEnquiry.Domain.Test
{
    internal static class RatesTestData
    {
        private static RateDto[] GetRatesData(string json) => JsonSerializer.Deserialize<RateDto[]>(json);

        public static RateDto[] GetCompleteRates()
        {
            const string json = @"[
  {
    ""from"": ""EUR"",
    ""to"": ""USD"",
    ""rate"": ""1.359""
  },
  {
    ""from"": ""CAD"",
    ""to"": ""EUR"",
    ""rate"": ""0.732""
  },
  {
    ""from"": ""USD"",
    ""to"": ""EUR"",
    ""rate"": ""0.736""
  },
  {
    ""from"": ""EUR"",
    ""to"": ""CAD"",
    ""rate"": ""1.366""
  }
]";
            return GetRatesData(json);
        }

        public static RateDto[] GetRatesDataWithotDataToCalculateAllRates()
        {
            const string json = @"[
  {
    ""from"": ""EUR"",
    ""to"": ""USD"",
    ""rate"": ""1.359""
  }
]";
            return GetRatesData(json);
        }
    }
}
