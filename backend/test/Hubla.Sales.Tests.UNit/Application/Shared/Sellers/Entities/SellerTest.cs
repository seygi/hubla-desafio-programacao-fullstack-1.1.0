using Hubla.Sales.Application.Shared.Sellers.Entities;

namespace Hubla.Sales.Tests.Unit.Application.Shared.Sellers.Entities
{
    public class SellerTest
    {
        [Fact(DisplayName = "ShouldCreateSeller")]
        public void ShouldCreateSeller()
        {
            // arrange
            string name = "Some name";

            // act
            var seller = Seller.Create(name);
            seller.Name.Should().Be(name);
        }
    }
}