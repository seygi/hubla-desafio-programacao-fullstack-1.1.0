using Hubla.Sales.Application.Shared.Sellers.Entities;
using Hubla.Sales.Application.Shared.UseCase;
using System.Diagnostics.CodeAnalysis;

namespace Hubla.Sales.Application.Features.GetSellers.UseCase
{
    [ExcludeFromCodeCoverage]
    public sealed class GetSellersListOutput : IOutput, IEnumerable<GetSellerOutput>
    {
        private readonly IList<GetSellerOutput> _sellersOutput;

        private GetSellersListOutput(IEnumerable<Seller> sellers)
        {
            _sellersOutput = new List<GetSellerOutput>();
            foreach (var seller in sellers)
            {
                var sales = GetSellerSalesListOutput.Create(seller.Sales.ToList());
                _sellersOutput.Add(GetSellerOutput.Create(seller.Id, seller.Name, sales));
            }
        }

        public static GetSellersListOutput Create(IEnumerable<Seller> sellers) => new(sellers ?? Array.Empty<Seller>());

        public IEnumerator<GetSellerOutput> GetEnumerator()
        {
            foreach (var seller in _sellersOutput)
                yield return seller;
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public static GetSellersListOutput Success(IEnumerable<Seller> sellers) => new(sellers);

        public static GetSellersListOutput Empty => new(Array.Empty<Seller>());
    }
}