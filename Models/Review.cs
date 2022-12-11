using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DetailingApi.Models;

public class Review
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    // [BsonElement("Reviewer")]
    public string reviewer { get; set; } = null!;

    public int rating { get; set; }

    public string reviewDate { get; set; } = null!;
    
    public string reviewText { get; set; } = null!;

    public string starCount { get; set; } = null!;
    public Boolean approved { get; set; }
}