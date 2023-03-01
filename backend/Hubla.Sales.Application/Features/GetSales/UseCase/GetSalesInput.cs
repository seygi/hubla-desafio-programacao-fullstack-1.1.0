using Hubla.Sales.Application.Shared.UseCase;

namespace Hubla.Sales.Application.Features.GetSales.UseCase
{
    public sealed class GetSalesInput : IInput
    {
        public byte[] File { get; set; }

        public GetSalesInput(byte[] file)
        {
            File = file;
        }

        public static GetSalesInput Create(byte[] file)
            => new(file);
    }
}
