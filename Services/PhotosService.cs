using DetailingApi.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace DetailingApi.Services;

public class PhotosService
{
    private readonly IMongoCollection<Photo> _photosCollection;

    public PhotosService(
        IOptions<DetailingDatabaseSettings> detailingDatabaseSettings)
    {
        var mongoClient = new MongoClient(
            detailingDatabaseSettings.Value.ConnectionString);

        var mongoDatabase = mongoClient.GetDatabase(
            detailingDatabaseSettings.Value.DatabaseName);

        _photosCollection = mongoDatabase.GetCollection<Photo>(
            detailingDatabaseSettings.Value.PhotosCollectionName);
    }

    public async Task<List<Photo>> GetAsync() =>
        await _photosCollection.Find(_ => true).ToListAsync();

    public async Task<Photo?> GetAsync(string id) =>
        await _photosCollection.Find(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(Photo newPhoto) =>
        await _photosCollection.InsertOneAsync(newPhoto);

    public async Task UpdateAsync(string id, Photo updatedPhoto) =>
        await _photosCollection.ReplaceOneAsync(x => x.Id == id, updatedPhoto);

    public async Task RemoveAsync(string id) =>
        await _photosCollection.DeleteOneAsync(x => x.Id == id);
}