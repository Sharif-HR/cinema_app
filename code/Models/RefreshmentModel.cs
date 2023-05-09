using System.Text.Json.Serialization;
public class RefreshmentModel
{
    [JsonPropertyName("id")]
    public string Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("isDrink")]
    public bool IsDrink { get; set; }

    [JsonPropertyName("price")]
    public double Price { get; set; }

    public RefreshmentModel(string name, bool isDrink,double price, string id = null){
        Id = string.IsNullOrWhiteSpace(id) ? Helpers.GenUid() : id;
        Name = name;
        IsDrink = isDrink;
        Price = price;
    }
}