using Hubla.Sales.Application.Shared.Data.Postgres;
using Hubla.Sales.Application.Shared.Sales.Repositories;
using Hubla.Sales.Application.Shared.Sellers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hubla.Sales.Tests.Unit.Application.Shared.Sellers.Repositories
{
    public class SellerRepositoryTest
    {
        private readonly SellerRepository _sellerRepository;

        public SellerRepositoryTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "hublasales")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            var context = new DataContext(options);
            _sellerRepository = new SellerRepository(context);
        }

        [Fact]
        public async Task AddSellerAsync_ShouldAdd_NewSeller_ToTheDatabase()
        {
            // Arrange
            var seller = Seller.Create("New name");

            // Act
            await _sellerRepository.SaveAsync(seller);

            // Assert
            var allSellers = await _sellerRepository.ListAsync();
            Assert.Single(allSellers);
            var addedSeller = allSellers.First();
            Assert.Equal(seller.Name, addedSeller.Name);
        }

        [Fact]
        public async Task GetSellerAsync_ShouldReturn_Seller_WithGivenId()
        {
            // Arrange
            var seller = Seller.Create("New name");

            await _sellerRepository.SaveAsync(seller);
            var sellerId = seller.Id;

            // Act
            var sellerDb = await _sellerRepository.GetByIdAsync(sellerId);

            // Assert
            Assert.NotNull(sellerDb);
            Assert.Equal(sellerDb.Id, seller.Id);
            Assert.Equal(sellerDb.Name, seller.Name);
        }

        [Fact]
        public async Task GetSellersAsync_ShouldReturn_AllSellers_InTheDatabase()
        {
            // Arrange
            var seller1 = Seller.Create("New name 1");
            var seller2 = Seller.Create("New name 2");

            await _sellerRepository.SaveAsync(seller1);
            await _sellerRepository.SaveAsync(seller2);

            // Act
            var list = await _sellerRepository.ListAsync();

            // Assert
            Assert.NotNull(list);
            Assert.Equal(2, list.Count());
        }
    }
}