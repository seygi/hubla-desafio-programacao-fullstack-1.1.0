using Hubla.Sales.Application.Shared.Sellers.UseCases.Outputs;

namespace Hubla.Sales.Application.Features.GetSellers.UseCase
{
    public class GetSellerOutput : SellerOutputBase
    {
        public GetSellerSalesListOutput Sales { get; set; }

        private GetSellerOutput(int id, string name, GetSellerSalesListOutput sales) : base(id, name)
        {
            this.Sales = sales;
        }

        public static GetSellerOutput Create(int id, string name, GetSellerSalesListOutput sales) =>
            new(id, name, sales);
    }

}
