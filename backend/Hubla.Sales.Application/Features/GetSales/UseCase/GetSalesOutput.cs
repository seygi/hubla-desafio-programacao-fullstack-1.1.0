using Hubla.Sales.Application.Shared.Sales.Enums;
using Hubla.Sales.Application.Shared.Sales.UseCases.Outputs;
using Hubla.Sales.Application.Shared.Sellers.UseCases.Outputs;

namespace Hubla.Sales.Application.Features.GetSales.UseCase
{
    public class GetSalesOutput : SaleOutputBase
    {
        private GetSalesOutput(int id, SaleType saleType, DateTime date, string description, double value, SellerOutputBase seller)
            : base(id, saleType, date, description, value, seller)
        {
        }

        public static GetSalesOutput Create(int id, SaleType saleType, DateTime date, string description, double value, SellerOutputBase seller) =>
            new(id, saleType, date, description, value, seller);
    }

}
