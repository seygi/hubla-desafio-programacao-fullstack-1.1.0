using Hubla.Sales.Application.Shared.Configurations;
using Hubla.Sales.Application.Shared.Configurations.DataBase;
using Hubla.Sales.Tests.Unit;

namespace Hubla.Sales.Tests.Unit.Application.Shared.Configurations
{
    public class ConnectionStringsTest
    {
        [Fact(DisplayName = "ShouldCreateObject")]
        public void ShouldCreateObject()
        {
            // arrange
            var postgres = new Postgres();

            // act
            var result = new ConnectionStrings { Postgres = postgres };

            // assert
            AssertProperties(result, postgres);
        }

        [Fact(DisplayName = "ShouldValidateProperties")]
        public void ShouldValidateProperties()
        {
            // arrange
            var expectedProperties = new List<AssertProperty>
            {
                new() {Name = "Postgres", Type = typeof(Postgres)},
            };

            // act - assert
            typeof(ConnectionStrings).ValidateProperties(expectedProperties);
        }

        private static void AssertProperties(ConnectionStrings result, Postgres postgres)
        {
            result.Postgres.Should().Be(postgres);
        }
    }
}