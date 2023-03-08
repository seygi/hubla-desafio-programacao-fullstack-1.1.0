using FluentValidation;
using FluentValidation.Results;
using Hubla.Sales.Application.Features.CreateSale.UseCase;
using Hubla.Sales.Application.Shared.Notifications;
using Hubla.Sales.Application.Shared.Validator;
using NSubstitute;
using System.Net;

namespace Hubla.Sales.Tests.Unit.Application.Shared.Validator
{
    public class ValidatorServiceTest
    {
        private readonly IValidatorService<CreateSaleInput> _validatorService;
        private readonly IValidator<CreateSaleInput> _validator;
        private readonly INotificationContext _notificationContext;

        private readonly byte[] _file;


        public ValidatorServiceTest()
        {
            _file = File.ReadAllBytes("sales.txt");

            _validator = Substitute.For<IValidator<CreateSaleInput>>();
            _notificationContext = Substitute.For<INotificationContext>();
            _validatorService = new ValidatorService<CreateSaleInput>(_validator, _notificationContext);
        }

        [Fact(DisplayName = "Should return true when input is valid with empty notifications")]
        public void ShouldReturnTrueWhenInputIsValidWithEmptyNotifications()
        {
            // arrange
            var input = new CreateSaleInput(_file);
            _validator
                .Validate(Arg.Is<CreateSaleInput>(t => t.File == _file))
                .Returns(new ValidationResult());

            // act
            var result = _validatorService.ValidateAndNotifyIfError(input);

            // assert
            result.Should().BeTrue();
            _notificationContext.DidNotReceiveWithAnyArgs().Create(Arg.Any<HttpStatusCode>(), Arg.Any<NotificationErrors>());
            _validator.Received().Validate(Arg.Is<CreateSaleInput>(t => t.File == _file));
        }

        [Fact(DisplayName = "Should Return False When Input Invalid With Notifications")]
        public void ShouldReturnFalseWhenInputInvalidWithNotifications()
        {
            // arrange
            var input = new CreateSaleInput(Array.Empty<byte>());
            _validator
                .Validate(Arg.Is<CreateSaleInput>(t => t == input))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                    new("File", "FailFile")
                }));

            var notificationErrorsExpected = NotificationErrors.Empty;
            notificationErrorsExpected.Add("File", "FailFile");

            Func<object, bool> validateContentParamNotificationContext = notificationResult =>
            {
                notificationResult.Should().BeEquivalentTo(notificationErrorsExpected);
                return true;
            };

            // act
            var result = _validatorService.ValidateAndNotifyIfError(input);

            // assert
            result.Should().BeFalse();
            _notificationContext.Received().Create(Arg.Is<HttpStatusCode>(t => t == HttpStatusCode.BadRequest), Arg.Is<NotificationErrors>(t => validateContentParamNotificationContext(t)));
            _validator.Received().Validate(Arg.Is<CreateSaleInput>(t => t == input));

        }

        [Fact(DisplayName = "Should Return True When Input Is Valid With Empty Validation Erros")]
        public void ShouldReturnTrueWhenInputIsValidWithEmptyValidationErros()
        {
            // arrange
            var input = new CreateSaleInput(_file);
            _validator
                .Validate(Arg.Is<CreateSaleInput>(t => t == input))
                .Returns(new ValidationResult());

            // act
            var result = _validatorService.Validate(input, out var notificationErrors);

            // assert
            result.Should().BeTrue();
            notificationErrors.Should().BeEquivalentTo(NotificationErrors.Empty);
            _validator.Received().Validate(Arg.Is<CreateSaleInput>(t => t == input));
            _notificationContext.DidNotReceiveWithAnyArgs().Create(Arg.Any<HttpStatusCode>(), Arg.Any<NotificationErrors>());
        }

        [Fact(DisplayName = "Should Return False When Input Invalid With Validation Errors")]
        public void ShouldReturnFalseWhenInputInvalidWithValidationErrors()
        {
            // arrange
            var input = new CreateSaleInput(Array.Empty<byte>());
            _validator
                .Validate(Arg.Is<CreateSaleInput>(t => t == input))
                .Returns(new ValidationResult(new List<ValidationFailure>
                {
                    new("File", "FailFile")
                }));

            var notificationErrorsExpected = NotificationErrors.Empty;
            notificationErrorsExpected.Add("File", "FailFile");

            // act
            var result = _validatorService.Validate(input, out var notificationErrors);

            // assert
            result.Should().BeFalse();
            notificationErrors.Should().BeEquivalentTo(notificationErrorsExpected);
            _validator.Received().Validate(Arg.Is<CreateSaleInput>(t => t == input));
            _notificationContext.Received(1).Create(HttpStatusCode.BadRequest, notificationErrors);
        }
    }
}