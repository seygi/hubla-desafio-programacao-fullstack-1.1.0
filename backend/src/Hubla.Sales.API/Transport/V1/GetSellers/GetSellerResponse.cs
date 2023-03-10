using Hubla.Sales.API.Transport.V1.Sellers;
using Hubla.Sales.Application.Features.GetSellers.UseCase;
using System.Text.Json.Serialization;

namespace Hubla.Sales.API.Transport.V1.GetSellers
{
    public sealed class GetSellerResponse : SellerResponseBase
    {
        [JsonPropertyName("sales")]
        public IEnumerable<GetSellerSalesResponse> Sales { get; set; }
        [JsonPropertyName("amountTotal")]
        public decimal AmountTotal { get; set; }
        public GetSellerResponse(int id, string name, IEnumerable<GetSellerSalesResponse> sales, decimal amountTotal) : base(id, name)
        {
            Sales = sales;
            AmountTotal = amountTotal;
        }

        public static IList<GetSellerResponse> Create(GetSellersListOutput outputUseCase)
        {
            return outputUseCase
                .Select(lnq => new GetSellerResponse(lnq.Id, lnq.Name, GetSellerSalesResponse.Create(lnq.Sales), lnq.AmountTotal))
               .ToList();
        }
    }
}