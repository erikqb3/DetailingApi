using DetailingApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DetailingApi.Services;

public class FeaturesService
{
    private readonly IMongoCollection<Feature> _featuresCollection;

    public FeaturesService(
        IOptions<DetailingDatabaseSettings> detailingDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            detailingDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            detailingDatabaseSettings.Value.DatabaseName);

        _featuresCollection = mongoDatabase.GetCollection<Feature>(
            detailingDatabaseSettings.Value.FeaturesCollectionName);
    }

    public async Task<List<Feature>> GetAsync() =>
        await _featuresCollection.Find(_ => true).ToListAsync();

    public async Task<Feature?> GetAsync(string id) =>
        await _featuresCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Feature newFeature) =>
        await _featuresCollection.InsertOneAsync(newFeature);

    public async Task UpdateAsync(string id, Feature updatedFeature) =>
        await _featuresCollection.ReplaceOneAsync(x => x.Id == id, updatedFeature);

    public async Task RemoveAsync(string id) =>
        await _featuresCollection.DeleteOneAsync(x => x.Id == id);
}