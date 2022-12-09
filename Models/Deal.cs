using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace DetailingApi.Models;

public class Deal
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    // [BsonElement("Deal")]
    // [JsonPropertyName("deal")]
    public string deal { get; set; } = null!;

    public string type { get; set; } = null!;

    public int price { get; set; }

    public string[] features_array { get; set; } = null!;
}