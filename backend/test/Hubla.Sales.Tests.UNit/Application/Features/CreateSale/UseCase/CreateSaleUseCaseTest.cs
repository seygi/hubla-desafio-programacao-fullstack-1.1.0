using Hubla.Sales.Application.Features.CreateSale.UseCase;
using Hubla.Sales.Application.Shared.Models;
using Hubla.Sales.Application.Shared.Notifications;
using Hubla.Sales.Application.Shared.Sales.Entities;
using Hubla.Sales.Application.Shared.Sales.Repositories;
using Hubla.Sales.Application.Shared.Sellers.Entities;
using Hubla.Sales.Application.Shared.Validator;
using NSubstitute;

namespace Hubla.Sales.Tests.Unit.Application.Features.CreateSale.UseCase
{
    public class CreateSaleUseCaseTest
    {
        private readonly IValidatorService<CreateSaleInput> _validatorService;
        private readonly INotificationContext _notificationContext;
        private readonly ISaleRepository _saleRepository;
        private readonly ISellerRepository _sellerRepository;

        private readonly CreateSaleUseCase _createSaleUseCase;

        public CreateSaleUseCaseTest()
        {
            _validatorService = Substitute.For<IValidatorService<CreateSaleInput>>();
            _notificationContext = Substitute.For<INotificationContext>();
            _saleRepository = Substitute.For<ISaleRepository>();
            _sellerRepository = Substitute.For<ISellerRepository>();

            _createSaleUseCase = new CreateSaleUseCase(
                _validatorService,
                _notificationContext,
                _saleRepository,
                _sellerRepository);
        }

        [Fact]
        public async Task HandleAsync_InvalidInput_ReturnsEmpty()
        {
            // Arrange
            var request = CreateSaleInput.Create(Array.Empty<byte>());


            _validatorService.ValidateAndNotifyIfError(Arg.Any<CreateSaleInput>()).Returns(false);

            // Act
            var response = await _createSaleUseCase.ExecuteAsync(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Should().BeEquivalentTo(CreateSaleOutput.Empty);
        }

        [Fact]
        public async Task HandleAsync_ValidInput_ReturnsSuccess()
        {
            // Arrange
            var file = File.ReadAllBytes("sales.txt");
            var request = CreateSaleInput.Create(file);
            var seller = Seller.Create("test");
            var output = OperationResult.Success(1);

            _validatorService.ValidateAndNotifyIfError(Arg.Any<CreateSaleInput>()).Returns(true);
            _sellerRepository.GetByNameAsync(Arg.Any<string>()).Returns(seller);
            _saleRepository.SaveAsync(Arg.Any<IList<Sale>>()).Returns(output);

            // Act
            var response = await _createSaleUseCase.ExecuteAsync(request, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            Assert.True(response.Success);
        }
    }
}
