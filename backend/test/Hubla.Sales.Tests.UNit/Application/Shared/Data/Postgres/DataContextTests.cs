using Hubla.Sales.Application.Shared.Data.Postgres;
using Hubla.Sales.Application.Shared.Sellers.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hubla.Sales.Tests.Unit.Application.Shared.Data.Postgres
{
    public class DataContextTests : IDisposable
    {
        private readonly DataContext _dataContext;

        public DataContextTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _dataContext = new DataContext(options);
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }

        [Fact]
        public void DataContext_Constructor_CreatesInstance()
        {
            Assert.NotNull(_dataContext);
        }

        [Fact]
        public void DataContext_Add_AddsEntity()
        {
            // Arrange
            var entity = Seller.Create("test");

            // Act
            _dataContext.Add(entity);
            _dataContext.SaveChanges();

            // Assert
            var result = _dataContext.Set<Seller>().FirstOrDefault(e => e.Id == entity.Id);
            Assert.NotNull(result);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public void DataContext_Find_FindsEntity()
        {
            // Arrange
            var entity = Seller.Create("test");
            _dataContext.Add(entity);
            _dataContext.SaveChanges();

            // Act
            var result = _dataContext.Find<Seller>(entity.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(entity.Name, result.Name);
        }

        [Fact]
        public void DataContext_AddRange_AddsMultipleEntities()
        {
            // Arrange
            var entities = new List<Seller>
            {
                Seller.Create("test 1"),
                Seller.Create("test 2"),
                Seller.Create("test 3")
            };

            // Act
            _dataContext.AddRange(entities);
            _dataContext.SaveChanges();

            // Assert
            var results = _dataContext.Set<Seller>().ToList();
            Assert.Equal(entities.Count, results.Count);
            foreach (var entity in entities)
            {
                var result = results.FirstOrDefault(e => e.Id == entity.Id);
                Assert.NotNull(result);
                Assert.Equal(entity.Name, result.Name);
            }
        }
    }
}