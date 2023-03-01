using Hubla.Sales.Application.Shared.Sales.Enums;

namespace Hubla.Sales.Application.Features.GetSales.UseCase
{
    public class GetSalesOutput
    {
        public int Id { get; set; }
        public SaleType SaleType { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public string SalesManName { get; set; }

        private GetSalesOutput(int id, SaleType saleType, DateTime date, string description, double value, string salesManName)
        {
            Id = id;
            SaleType = saleType;
            Date = date;
            Description = description;
            Value = value;
            SalesManName = salesManName;
        }

        public static GetSalesOutput Create(int id, SaleType saleType, DateTime date, string description, double value, string salesmanName) =>
            new(id, saleType, date, description, value, salesmanName);
    }

}
