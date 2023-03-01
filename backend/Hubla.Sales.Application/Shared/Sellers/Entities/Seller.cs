using Hubla.Sales.Application.Shared.Sales.Entities;

namespace Hubla.Sales.Application.Shared.Sellers.Entities
{
    public class Seller
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Sale> Sales { get; set; }

    }
}
