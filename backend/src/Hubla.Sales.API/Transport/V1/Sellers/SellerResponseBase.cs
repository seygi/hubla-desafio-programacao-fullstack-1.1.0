using System.Text.Json.Serialization;

namespace Hubla.Sales.API.Transport.V1.Sellers
{
    public abstract class SellerResponseBase
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonConstructor]
        public SellerResponseBase(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
