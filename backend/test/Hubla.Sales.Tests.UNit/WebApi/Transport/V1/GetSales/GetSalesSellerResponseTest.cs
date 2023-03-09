using Hubla.Sales.API.Transport.V1.GetSales;

namespace Hubla.Sales.Tests.Unit.WebApi.Transport.V1.GetSales
{
    public class GetSalesSellerResponseTest
    {
        [Fact]
        public void ShouldCreateObject()
        {
            // arrange - act
            (int id, string name) data =
                (1, "some name");

            var result = new GetSalesSellerResponse(data.id, data.name);
            result.Id.Should().Be(data.id);
            result.Name.Should().Be(data.name);
        }
    }
}