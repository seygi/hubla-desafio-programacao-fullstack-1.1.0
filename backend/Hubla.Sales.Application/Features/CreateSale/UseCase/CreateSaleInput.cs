using Hubla.Sales.Application.Shared.UseCase;

namespace Hubla.Sales.Application.Features.CreateSale.UseCase
{
    public sealed class CreateSaleInput : IInput
    {
        public byte[] File { get; set; }

        public CreateSaleInput(byte[] file)
        {
            File = file;
        }

        public static CreateSaleInput Create(byte[] file)
            => new(file);
    }
}
