using Hubla.Sales.Application.Shared.Data.Postgres;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.Sales.Enums;
using Hubla.Sales.Application.Shared.Sales.Repositories;
using Hubla.Sales.Application.Shared.Sellers.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Hubla.Sales.Tests.Unit.Application.Shared.Sales.Repositories
{
    public class SaleRepositoryTest
    {
        private readonly SaleRepository _saleRepository;
        private readonly DataContext _dataContext;

        public SaleRepositoryTest()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "hublasales")
                .UseInternalServiceProvider(serviceProvider)
                .Options;

            _dataContext = new DataContext(options);
            _saleRepository = new SaleRepository(_dataContext);
        }

        [Fact]
        public async Task AddSaleAsync_ShouldAdd_NewSale_ToTheDatabase()
        {
            // Arrange
            var entity = Seller.Create("test");
            _dataContext.Add(entity);
            _dataContext.SaveChanges();

            (SaleType saleType, DateTime date, string description, decimal value) data =
                (SaleType.ProducerSale, DateTime.Now, "some description", 125M);
            var sale = Sale.Create(data.saleType, data.date, data.description, data.value, entity);

            // Act
            await _saleRepository.SaveAsync(new List<Sale> { sale });

            // Assert
            var allSales = await _saleRepository.ListAsync();
            Assert.Single(allSales);
            var addedSale = allSales.First();
            Assert.Equal(sale.SaleType, addedSale.SaleType);
            Assert.Equal(sale.Date, addedSale.Date);
            Assert.Equal(sale.Description, addedSale.Description);
            Assert.Equal(sale.Value, addedSale.Value);
        }

        [Fact]
        public async Task GetSalesAsync_ShouldReturn_AllSales_InTheDatabase()
        {
            // Arrange
            var entity = Seller.Create("test");
            _dataContext.Add(entity);
            _dataContext.SaveChanges();

            var sale1 = Sale.Create(SaleType.ProducerSale, DateTime.Now, "some description 1", 125M, entity);
            var sale2 = Sale.Create(SaleType.ProducerSale, DateTime.Now, "some description 2", 125M, entity);

            await _saleRepository.SaveAsync(new List<Sale> { sale1, sale2 });

            // Act
            var list = await _saleRepository.ListAsync();

            // Assert
            Assert.NotNull(list);
            Assert.Equal(2, list.Count());
        }
    }
}