using GNB.SalesEnquiry.Shared.Enums;
using System.Text.Json.Serialization;

namespace GNB.SalesEnquiry.Shared.Dtos
{
    public class TransactionDto
    {
        [JsonPropertyName("sku")]
        public string Sku { get; set; }
        [JsonPropertyName("amount")]
        [JsonConverter(typeof(JsonDecimalConverter))]
        public decimal Amount { get; set; }
        [JsonPropertyName("currency")]
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public CurrencyTypesEnum Currency { get; set; }
    }
}
