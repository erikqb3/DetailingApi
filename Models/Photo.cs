using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DetailingApi.Models;

public class Photo
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    public string page { get; set; } = null!;
    public string category { get; set; } = null!;
    public string type { get; set; } = null!;
    
    public string alt { get; set; } = null!;

    public string path { get; set; } = null!;
}