using Hubla.Sales.API.Transport.V1.Sales;
using Hubla.Sales.Application.Features.GetSales.UseCase;
using Hubla.Sales.Application.Shared.Sales.Enums;
using System.Text.Json.Serialization;

namespace Hubla.Sales.API.Transport.V1.GetSales
{
    public sealed class GetSalesResponse : SaleResponseBase
    {
        public GetSalesSellerResponse Seller { get; set; }

        public GetSalesResponse(int id, SaleType saleType, DateTime date, string description, double value, GetSalesSellerResponse seller)
            : base(id, saleType, date, description, value)
        {
            Seller = seller;
        }

        public static IList<GetSalesResponse> Create(GetSalesListOutput outputUseCase) =>
            outputUseCase.Select(lnq => new GetSalesResponse(lnq.Id, lnq.SaleType, lnq.Date, lnq.Description, lnq.Value, GetSalesSellerResponse.Create(lnq.Seller.Id, lnq.Seller.Name)))
               .ToList();
    }
}