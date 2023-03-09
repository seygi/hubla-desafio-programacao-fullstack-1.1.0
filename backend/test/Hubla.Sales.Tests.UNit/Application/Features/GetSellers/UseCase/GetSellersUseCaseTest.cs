using Hubla.Sales.Application.Features.GetSales.UseCase;
using Hubla.Sales.Application.Features.GetSellers.UseCase;
using Hubla.Sales.Application.Shared.Notifications;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.Sales.Enums;
using Hubla.Sales.Application.Shared.Sales.Repositories;
using Hubla.Sales.Application.Shared.Sellers.Entities;
using Hubla.Sales.Application.Shared.UseCase;
using NSubstitute;
using System.Net;

namespace Hubla.Sales.Tests.Unit.Application.Features.GetSellers.UseCase
{
    public class GetSellersUseCaseTest
    {
        private readonly INotificationContext _notificationContext;
        private readonly ISellerRepository _sellerRepository;

        private readonly GetSellersUseCase _getSellersUseCase;

        public GetSellersUseCaseTest()
        {
            _notificationContext = Substitute.For<INotificationContext>();
            _sellerRepository = Substitute.For<ISellerRepository>();

            _getSellersUseCase = new GetSellersUseCase(_notificationContext, _sellerRepository);
        }

        [Fact]
        public async Task HandleAsync_WithoutDataInDatabase_ReturnsEmpty()
        {
            // Arrange
            var emptyList = new List<Seller>();
            _sellerRepository.ListAsync().Returns(emptyList);

            // Act
            var response = await _getSellersUseCase.ExecuteAsync(DefaultInput.Default, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(GetSalesListOutput.Empty);
        }

        [Fact]
        public async Task HandleAsync_WithInDatabase_ReturnsSuccess()
        {
            // Arrange
            var listSales1 = new List<Sale>()
            {
                Sale.CreateWithoutSeller(SaleType.ProducerSale, DateTime.Now, "some description 1", 125M),
                Sale.CreateWithoutSeller(SaleType.AffiliateSale, DateTime.Now, "some description 2", 250M)
            };
            var listSales2 = new List<Sale>()
            {
                Sale.CreateWithoutSeller(SaleType.ProducerSale, DateTime.Now, "some description 3", 135M),
                Sale.CreateWithoutSeller(SaleType.PaidComission, DateTime.Now, "some description 4", 5M)
            };

            var list = new List<Seller>()
            {
                Seller.Create(1, "some name 1", listSales1),
                Seller.Create(2, "some name 2", listSales2)
            };
            _sellerRepository.ListAsync().Returns(list);

            // Act
            var response = await _getSellersUseCase.ExecuteAsync(DefaultInput.Default, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            Assert.Equal(2, response.Count());
        }
    }
}
