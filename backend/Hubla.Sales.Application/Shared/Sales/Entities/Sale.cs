using Hubla.Sales.Application.Shared.Sales.Enums;
using Hubla.Sales.Application.Shared.Sellers.Entities;

namespace Hubla.Sales.Application.Shared.Sales.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public SaleType SaleType { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Value { get; set; }
        public Seller Seller { get; set; }
    }
}