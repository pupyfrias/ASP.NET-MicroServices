using Catalog.API.Entities;
using Catalog.API.Interfaces;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
    {
        public CatalogContext(IConfiguration configuration)
        {
            string? connectionString = configuration["DataBaseSettings:ConnectionString"];
            string? dataBaseName = configuration["DataBaseSettings:DataBaseName"];
            string? collectionName = configuration["DataBaseSettings:CollectionName"];

            Console.WriteLine(connectionString);
            var client = new MongoClient(connectionString);
            var dataBase = client.GetDatabase(dataBaseName);
            Products = dataBase.GetCollection<Product>(collectionName);
            CatalogContextSeed.SeedData(Products);
        }

        public IMongoCollection<Product> Products { get; }
    }

}
