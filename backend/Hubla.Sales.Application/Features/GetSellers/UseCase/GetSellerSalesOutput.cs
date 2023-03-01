using Hubla.Sales.Application.Shared.Sales.Enums;
using Hubla.Sales.Application.Shared.Sales.UseCases.Outputs;
using Hubla.Sales.Application.Shared.Sellers.UseCases.Outputs;

namespace Hubla.Sales.Application.Features.GetSellers.UseCase
{
    public class GetSellerSalesOutput : SaleOutputBase
    {
        private GetSellerSalesOutput(int id, SaleType saleType, DateTime date, string description, double value, SellerOutputBase seller)
            : base(id, saleType, date, description, value, seller)

        {
        }

        public static GetSellerSalesOutput Create(int id, SaleType saleType, DateTime date, string description, double value, SellerOutputBase seller) =>
            new(id, saleType, date, description, value, seller);
    }

}
