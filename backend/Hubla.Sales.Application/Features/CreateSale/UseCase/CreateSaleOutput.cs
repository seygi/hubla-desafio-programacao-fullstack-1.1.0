using Hubla.Sales.Application.Shared.UseCase;

namespace Hubla.Sales.Application.Features.CreateSale.UseCase
{
    public sealed class CreateSaleOutput : IOutput
    {
        public bool Success { get; }

        private CreateSaleOutput(bool success)
        {
            Success = success;
        }

        public static CreateSaleOutput Create(bool success) => new(success);

        public static CreateSaleOutput Empty => new(false);
    }
}
