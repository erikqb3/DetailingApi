using DetailingApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DetailingApi.Services;

public class ReviewsService
{
    private readonly IMongoCollection<Review> _reviewsCollection;

    public ReviewsService(
        IOptions<DetailingDatabaseSettings> detailingDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            detailingDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            detailingDatabaseSettings.Value.DatabaseName);

        _reviewsCollection = mongoDatabase.GetCollection<Review>(
            detailingDatabaseSettings.Value.ReviewsCollectionName);
    }

    public async Task<List<Review>> GetAsync() =>
        await _reviewsCollection.Find(_ => true).ToListAsync();

    public async Task<Review?> GetAsync(string id) =>
        await _reviewsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Review newReview) =>
        await _reviewsCollection.InsertOneAsync(newReview);

    public async Task UpdateAsync(string id, Review updatedReview) =>
        await _reviewsCollection.ReplaceOneAsync(x => x.Id == id, updatedReview);

    public async Task RemoveAsync(string id) =>
        await _reviewsCollection.DeleteOneAsync(x => x.Id == id);
}