using Hubla.Sales.API.Transport.V1.Sellers;

namespace Hubla.Sales.API.Transport.V1.GetSales
{
    public sealed class GetSalesSellerResponse : SellerResponseBase
    {
        public GetSalesSellerResponse(int id, string name) : base(id, name)
        {
        }

        public static GetSalesSellerResponse Create(int id, string name) =>
            new(id, name);
    }
}