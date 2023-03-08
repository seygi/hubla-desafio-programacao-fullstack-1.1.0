using Hubla.Sales.Application.Shared.Models;

namespace Hubla.Sales.Tests.Unit.Application.Shared.Models
{
    public class OperationResultTest
    {
        public bool HasRowsAffected { get; }

        public bool HasUnexpectedError { get; }

        public int RowsAffected { get; }
        [Fact(DisplayName = "ShouldCreateOperationResult")]
        public void ShouldCreateOperationResult()
        {
            // arrange
            var rowsAffected = 1;
            var result = OperationResult.Success(rowsAffected);

            // act
            result.RowsAffected.Should().Be(rowsAffected);
            Assert.True(result.HasRowsAffected);
            Assert.False(result.HasUnexpectedError);
        }
    }
}