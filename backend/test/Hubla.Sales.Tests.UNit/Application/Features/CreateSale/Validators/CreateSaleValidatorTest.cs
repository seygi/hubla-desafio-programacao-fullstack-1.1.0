using FluentValidation;
using Hubla.Sales.Application.Features.CreateSale.UseCase;
using Hubla.Sales.Application.Features.CreateSale.Validators;

namespace Hubla.Sales.Tests.Unit.Application.Features.CreateSale.Validators
{
    public class CreateSaleValidatorTest
    {
        private readonly CreateSaleValidator _validator;

        public CreateSaleValidatorTest()
        {
            _validator = new CreateSaleValidator();
        }

        [Fact]
        public void Should_Have_Error_When_File_Null_Or_Empty()
        {
            var model = CreateSaleInput.Create(Array.Empty<byte>());
            var result = _validator.Validate(model);

            // assert
            result.IsValid.Should().BeFalse();
            result.Errors.Where(x => x.PropertyName == "File").Count().Should().Be(1);
        }

        [Fact]
        public void Should_Return_True_When_Has_All_Correct_Data()
        {
            var file = File.ReadAllBytes("sales.txt");
            var model = CreateSaleInput.Create(file);
            var result = _validator.Validate(model);

            result.IsValid.Should().BeTrue();
            result.Errors.Count().Should().Be(0);
        }
    }
}
