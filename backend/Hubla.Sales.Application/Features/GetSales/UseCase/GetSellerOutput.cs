namespace Hubla.Sales.Application.Features.GetSales.UseCase
{
    public class GetSellerOutput
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private GetSellerOutput(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static GetSellerOutput Create(int id, string name) =>
            new(id, name);
    }

}
