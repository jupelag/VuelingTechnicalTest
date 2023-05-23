using GNB.SalesEnquiry.Shared.Enums;
using System.Text.Json.Serialization;

namespace GNB.SalesEnquiry.Shared.Dtos
{
    public class RateDto
    {
        public RateDto(CurrencyTypesEnum from, CurrencyTypesEnum to, decimal rate)
        {
            From = from;
            To = to;
            Rate = rate;
        }

        public RateDto (){
        }

        [JsonPropertyName("from")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CurrencyTypesEnum From { get; set; }
        [JsonPropertyName("to")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CurrencyTypesEnum To { get; set; }
        [JsonPropertyName("rate")]
        [JsonConverter(typeof(JsonDecimalBankersRoudingConverter))]
        public decimal Rate { get; set; }
    }
}
