using Catalog.API.Entities;
using Catalog.API.Interfaces;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ICatalogContext _catalogContext;


        public ProductRepository(ICatalogContext catalogContext)
        {
            _catalogContext = catalogContext;
        }

        public async Task CreateAsync(Product product)
        {
           await _catalogContext.Products.InsertOneAsync(product);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var deleteResult = await _catalogContext.Products.DeleteOneAsync(product=> product.Id.Equals(id));
            return deleteResult.IsAcknowledged && deleteResult.DeletedCount > 0;
        }

        public async Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return await _catalogContext.Products.Find(product=> true).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByCategoryNameAsync(string categoryName)
        {
            return await _catalogContext.Products.Find(product=> product.Category == categoryName).ToListAsync();
        }

        public async Task<Product?> GetProductByIdAsync(string id)
        {
            return await _catalogContext.Products.Find(product=> product.Id.Equals(id)).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            return await _catalogContext.Products.Find(product => product.Name == name).ToListAsync();
        }

        public async Task<bool> UpdateAsync(Product product)
        {

            var updateResult = await _catalogContext.Products.ReplaceOneAsync(p => p.Id.Equals(product.Id), product);
            return updateResult.IsAcknowledged && updateResult.ModifiedCount > 0;
        }
    }
}
