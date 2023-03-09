using Hubla.Sales.Application.Features.GetSales.UseCase;
using Hubla.Sales.Application.Shared.Notifications;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.Sales.Enums;
using Hubla.Sales.Application.Shared.Sales.Repositories;
using Hubla.Sales.Application.Shared.Sellers.Entities;
using Hubla.Sales.Application.Shared.UseCase;
using NSubstitute;

namespace Hubla.Sales.Tests.Unit.Application.Features.GetSales.UseCase
{
    public class GetSalesUseCaseTest
    {
        private readonly INotificationContext _notificationContext;
        private readonly ISaleRepository _saleRepository;

        private readonly GetSalesUseCase _getSalesUseCase;
        public GetSalesUseCaseTest()
        {
            _notificationContext = Substitute.For<INotificationContext>();
            _saleRepository = Substitute.For<ISaleRepository>();

            _getSalesUseCase = new GetSalesUseCase(_notificationContext, _saleRepository);
        }

        [Fact]
        public async Task HandleAsync_WithoutDataInDatabase_ReturnsEmpty()
        {
            // Arrange
            var emptyList = new List<Sale>();
            _saleRepository.ListAsync().Returns(emptyList);

            // Act
            var response = await _getSalesUseCase.ExecuteAsync(DefaultInput.Default, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(GetSalesListOutput.Empty);
        }

        [Fact]
        public async Task HandleAsync_WithInDatabase_ReturnsSuccess()
        {
            // Arrange
            var seller = Seller.Create("test");
            var list = new List<Sale>()
            {
                Sale.Create(SaleType.ProducerSale, DateTime.Now, "some description 1", 125M, seller),
                Sale.Create(SaleType.ProducerSale, DateTime.Now, "some description 2", 125M, seller)
            };
            _saleRepository.ListAsync().Returns(list);

            // Act
            var response = await _getSalesUseCase.ExecuteAsync(DefaultInput.Default, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            Assert.Equal(2, response.Count());
        }
    }
}
