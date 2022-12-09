using DetailingApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DetailingApi.Services;

public class DealsService
{
    private readonly IMongoCollection<Deal> _dealsCollection;

    public DealsService(
        IOptions<DetailingDatabaseSettings> detailingDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            detailingDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            detailingDatabaseSettings.Value.DatabaseName);

        _dealsCollection = mongoDatabase.GetCollection<Deal>(
            detailingDatabaseSettings.Value.DealsCollectionName);
    }

    public async Task<List<Deal>> GetAsync() =>
        await _dealsCollection.Find(_ => true).ToListAsync();

    public async Task<Deal?> GetAsync(string id) =>
        await _dealsCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Deal newDeal) =>
        await _dealsCollection.InsertOneAsync(newDeal);

    public async Task UpdateAsync(string id, Deal updatedDeal) =>
        await _dealsCollection.ReplaceOneAsync(x => x.Id == id, updatedDeal);

    public async Task RemoveAsync(string id) =>
        await _dealsCollection.DeleteOneAsync(x => x.Id == id);
}