using DetailingApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DetailingApi.Services;

public class ContactService
{
    private readonly IMongoCollection<Contact> _contactCollection;

    public ContactService(
        IOptions<DetailingDatabaseSettings> detailingDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            detailingDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            detailingDatabaseSettings.Value.DatabaseName);

        _contactCollection = mongoDatabase.GetCollection<Contact>(
            detailingDatabaseSettings.Value.ContactCollectionName);
    }

    public async Task<List<Contact>> GetAsync() =>
        await _contactCollection.Find(_ => true).ToListAsync();

    public async Task<Contact?> GetAsync(string id) =>
        await _contactCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Contact newContact) =>
        await _contactCollection.InsertOneAsync(newContact);

    public async Task UpdateAsync(string id, Contact updatedContact) =>
        await _contactCollection.ReplaceOneAsync(x => x.Id == id, updatedContact);

    public async Task RemoveAsync(string id) =>
        await _contactCollection.DeleteOneAsync(x => x.Id == id);
}