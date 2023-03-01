using System.Text.Json.Serialization;

namespace Hubla.Sales.API.Transport.V1.GetSales
{
    public sealed class GetSellerResponse
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonConstructor]
        public GetSellerResponse(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public static GetSellerResponse Create(int id, string name) =>
            new(id, name);
    }
}