using Hubla.Sales.API.Transport.V1.Sales;
using Hubla.Sales.Application.Features.GetSellers.UseCase;
using Hubla.Sales.Application.Shared.Sales.Enums;

namespace Hubla.Sales.API.Transport.V1.GetSellers
{
    public sealed class GetSellerSalesResponse : SaleResponseBase
    {
        public GetSellerSalesResponse(int id, SaleType saleType, DateTime date, string description, double value)
            : base(id, saleType, date, description, value)
        {
        }

        public static IList<GetSellerSalesResponse> Create(GetSellerSalesListOutput outputUseCase) =>
            outputUseCase.Select(lnq => new GetSellerSalesResponse(lnq.Id, lnq.SaleType, lnq.Date, lnq.Description, lnq.Value))
               .ToList();
    }
}