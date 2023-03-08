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
        public decimal Value { get; set; }
        public Seller Seller { get; set; }
        protected Sale() { }
        private Sale(SaleType saleType, DateTime date, string description, decimal value, Seller seller)
        {
            SaleType = saleType;
            Date = date;
            Description = description;
            Value = value;
            Seller = seller;
        }

        public static Sale Create(SaleType saleType, DateTime date, string description, decimal value, Seller seller)
            => new(saleType, date, description, value, seller);
        public static Sale CreateWithoutSeller(SaleType saleType, DateTime date, string description, decimal value)
            => new(saleType, date, description, value, null);
    }
}