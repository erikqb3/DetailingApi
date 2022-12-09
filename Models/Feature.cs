using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DetailingApi.Models;

public class Feature
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    // [BsonElement("Name")]
    public string name { get; set; } = null!;
}