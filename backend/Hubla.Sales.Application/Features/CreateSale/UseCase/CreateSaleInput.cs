using Hubla.Sales.Application.Shared.UseCase;

namespace Hubla.Sales.Application.Features.CreateSale.UseCase
{
    public sealed class CreateSaleInput : IInput
    {
        public byte[] Buffer { get; set; }

        public CreateSaleInput(byte[] buffer)
        {
            Buffer = buffer;
        }

        public static CreateSaleInput Create(byte[] buffer)
            => new(buffer);
    }
}
