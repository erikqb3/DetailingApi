namespace DetailingApi.Models;

public class DetailingDatabaseSettings
{
    public string ConnectionString { get; set; } = null!;

    public string DatabaseName { get; set; } = null!;

    public string ContactCollectionName { get; set; } = null!;
    public string DealsCollectionName { get; set; } = null!;
    public string FeaturesCollectionName { get; set; } = null!;
    public string PhotosCollectionName { get; set; } = null!;
    public string ReviewsCollectionName { get; set; } = null!;
}