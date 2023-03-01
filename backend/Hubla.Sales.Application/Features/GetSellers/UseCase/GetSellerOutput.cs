using Hubla.Sales.Application.Shared.Sellers.UseCases.Outputs;

namespace Hubla.Sales.Application.Features.GetSellers.UseCase
{
    public class GetSellerOutput : SellerOutputBase
    {

        private GetSellerOutput(int id, string name) : base(id, name)
        {
        }

        public static GetSellerOutput Create(int id, string name) =>
            new(id, name);
    }

}
