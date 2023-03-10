using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.Application.Shared.Sellers.UseCases.Outputs
{
    [ExcludeFromCodeCoverage]
    public abstract class SellerOutputBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        protected SellerOutputBase(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
