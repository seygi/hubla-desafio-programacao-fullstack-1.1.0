using Hubla.Sales.Application.Features.GetSales.UseCase;
using Hubla.Sales.Application.Shared.Sales.Enums;
using System.Text.Json.Serialization;

namespace Hubla.Sales.API.Transport.V1.GetSales
{
    public sealed class GetSalesResponse
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
        [JsonPropertyName("salesManName")]
        public string SalesManName { get; set; }

        [JsonConstructor]
        public GetSalesResponse(int id, SaleType saleType, DateTime date, string description, double value, string salesManName)
        {
            Id = id;
            SaleType = saleType;
            Date = date;
            Description = description;
            Value = value;
            SalesManName = salesManName;
        }

        public static IList<GetSalesResponse> Create(GetSalesListOutput outputUseCase) =>
            outputUseCase.Select(lnq => new GetSalesResponse(lnq.Id, lnq.SaleType, lnq.Date, lnq.Description, lnq.Value, lnq.SalesManName))
               .ToList();
    }
}