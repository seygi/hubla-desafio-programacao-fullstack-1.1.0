using Hubla.Sales.Application.Shared.Sellers.UseCases.Outputs;

namespace Hubla.Sales.Application.Features.GetSellers.UseCase
{
    public class GetSellerOutput : SellerOutputBase
    {
        public GetSellerSalesListOutput Sales { get; set; }
        public decimal AmountTotal
        {
            get
            {
                var amount = 0m;
                foreach (var sale in Sales)
                {
                    switch (sale.SaleType)
                    {
                        case Shared.Sales.Enums.SaleType.ProducerSale:
                        case Shared.Sales.Enums.SaleType.AffiliateSale:
                        case Shared.Sales.Enums.SaleType.ReceivedComission:
                            amount += sale.Value;
                            break;
                        case Shared.Sales.Enums.SaleType.PaidComission:
                            amount -= sale.Value;
                            break;
                    }
                }

                return amount;
            }
        }

        private GetSellerOutput(int id, string name, GetSellerSalesListOutput sales) : base(id, name)
        {
            this.Sales = sales;
        }

        public static GetSellerOutput Create(int id, string name, GetSellerSalesListOutput sales) =>
            new(id, name, sales);
    }

}
