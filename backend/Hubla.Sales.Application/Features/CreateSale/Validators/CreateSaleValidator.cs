using FluentValidation;
using Hubla.Sales.Application.Features.CreateSale.UseCase;
using Hubla.Sales.Application.Features.GetSales.UseCase;

namespace Hubla.Sales.Application.Features.CreateSale.Validators
{
    internal class CreateSaleValidator : AbstractValidator<CreateSaleInput>
    {
        private const string EmptyPropertyErrorMessage = "The property {PropertyName} cannot be null or empty.";

        public CreateSaleValidator()
        {
            RuleFor(i => i.File)
                .NotEmpty()
                .WithMessage(EmptyPropertyErrorMessage)
                .NotNull()
                .WithMessage(EmptyPropertyErrorMessage);
        }
    }
}
