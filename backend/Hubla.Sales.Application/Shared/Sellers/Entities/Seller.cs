using Hubla.Sales.Application.Shared.Sales.Entities;

namespace Hubla.Sales.Application.Shared.Sellers.Entities
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Sale> Sales { get; set; }

        protected Seller() { }
        private Seller(int id, string name)
        {
            Id = id;
            Name = name;
        }
        private Seller(int id, string name, ICollection<Sale> sales)
        {
            Id = id;
            Name = name;
            Sales = sales;
        }

        public static Seller Create(string name)
            => new(0, name);

        public static Seller Create(int id, string name, ICollection<Sale> sales)
            => new(id, name, sales);
    }
}