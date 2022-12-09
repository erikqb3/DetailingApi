using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DetailingApi.Models;

public class Contact
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    // [BsonElement("Name")]
    public string name { get; set; } = null!;

    public string phone { get; set; } = null!;

    public string email { get; set; } = null!;

    public string address { get; set; } = null!;

    public string facebook { get; set; } = null!;
}