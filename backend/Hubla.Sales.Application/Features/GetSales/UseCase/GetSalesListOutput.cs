using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.UseCase;
using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.Application.Features.GetSales.UseCase
{
    [ExcludeFromCodeCoverage]
    public sealed class GetSalesListOutput : IOutput, IEnumerable<GetSalesOutput>
    {
        private readonly IList<GetSalesOutput> _salesOutput;

        private GetSalesListOutput(IEnumerable<Sale> sales)
        {
            _salesOutput = new List<GetSalesOutput>();
            foreach (var sale in sales)
                _salesOutput.Add(GetSalesOutput.Create(sale.Id, sale.SaleType, sale.Date, sale.Description, sale.Value, sale.SellerName));
        }

        public static GetSalesListOutput Create(IEnumerable<Sale> sales) => new(sales ?? Array.Empty<Sale>());

        public IEnumerator<GetSalesOutput> GetEnumerator()
        {
            foreach (var sale in _salesOutput)
                yield return sale;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static GetSalesListOutput Success(IEnumerable<Sale> sales) => new(sales);

        public static GetSalesListOutput Empty => new(Array.Empty<Sale>());
    }
}