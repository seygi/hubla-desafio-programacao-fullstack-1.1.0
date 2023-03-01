using Hubla.Sales.Application.Shared.Sales.Enums;
using System.Text.Json.Serialization;

namespace Hubla.Sales.API.Transport.V1.Sales
{
    public abstract class SaleResponseBase
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("saleType")]
        public SaleType SaleType { get; set; }
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
        [JsonPropertyName("description")]
        public string Description { get; set; }
        [JsonPropertyName("value")]
        public double Value { get; set; }

        [JsonConstructor]
        public SaleResponseBase(int id, SaleType saleType, DateTime date, string description, double value)
        {
            Id = id;
            SaleType = saleType;
            Date = date;
            Description = description;
            Value = value;
        }
    }
}
