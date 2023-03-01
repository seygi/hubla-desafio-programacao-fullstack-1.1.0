using Hubla.Sales.Application.Shared.Sales.Enums;
using Hubla.Sales.Application.Shared.Sellers.UseCases.Outputs;

namespace Hubla.Sales.Application.Shared.Sales.UseCases.Outputs
{
    public abstract class SaleOutputBase
    {
        public int Id { get; set; }
        public SaleType SaleType { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public SellerOutputBase Seller { get; set; }

        protected SaleOutputBase(int id, SaleType saleType, DateTime date, string description, double value, SellerOutputBase seller)
        {
            Id = id;
            SaleType = saleType;
            Date = date;
            Description = description;
            Value = value;
            Seller = seller;
        }
    }
}
