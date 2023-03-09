using Hubla.Sales.Application.Features.GetSales.UseCase;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.Sales.UseCases.Outputs;
using Hubla.Sales.Application.Shared.UseCase;

namespace Hubla.Sales.Application.Features.GetSellers.UseCase
{
    public sealed class GetSellerSalesListOutput : IOutput, IEnumerable<SaleOutputBase>
    {
        private readonly IList<SaleOutputBase> _salesOutput;

        private GetSellerSalesListOutput(IEnumerable<Sale> sales)
        {
            _salesOutput = new List<SaleOutputBase>();
            foreach (var sale in sales)
                _salesOutput.Add(GetSalesOutput.Create(sale.Id, sale.SaleType, sale.Date, sale.Description, sale.Value, null));
        }

        public static GetSellerSalesListOutput Create(IEnumerable<Sale> sales) => new(sales ?? Array.Empty<Sale>());

        public IEnumerator<SaleOutputBase> GetEnumerator()
        {
            foreach (var sale in _salesOutput)
                yield return sale;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static GetSellerSalesListOutput Success(IEnumerable<Sale> sales) => new(sales);

        public static GetSellerSalesListOutput Empty => new(Array.Empty<Sale>());
    }
}
