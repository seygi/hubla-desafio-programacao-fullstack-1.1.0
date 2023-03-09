using Hubla.Sales.Application.Shared.Sellers.UseCases.Outputs;

namespace Hubla.Sales.Application.Features.GetSales.UseCase
{
    public class GetSalesSellerOutput : SellerOutputBase
    {

        private GetSalesSellerOutput(int id, string name) : base(id, name)
        {
        }

        public static GetSalesSellerOutput Create(int id, string name) =>
            new(id, name);
    }

}
