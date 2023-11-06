using Catalog.API.Entities;
using Catalog.API.Interfaces;
using MongoDB.Bson;

namespace Catalog.API.Services
{
    public class ProductService : IProductService
    {

        private readonly IProductRepository _productRepository;
        private readonly ILogger<ProductService> _logger;

        public ProductService(IProductRepository productRepository, ILogger<ProductService> logger)
        {
            _productRepository = productRepository;
            _logger = logger;

        }
        public async Task CreateAsync(Product product)
        {
            await _productRepository.CreateAsync(product);
        }

        public async Task DeleteAsync(string id)
        {
            var product = await GetProductByIdAsync(id);
            if(product == null) { throw new Exception(); }

           await  _productRepository.DeleteAsync(id);
        }

        public Task<IEnumerable<Product>> GetAllProductsAsync()
        {
            return _productRepository.GetAllProductsAsync();
        }

        public Task<IEnumerable<Product>> GetProductByCategoryNameAsync(string categoryName)
        {
            return _productRepository.GetProductByCategoryNameAsync(categoryName);
        }

        public async Task<Product?> GetProductByIdAsync(string id)
        {
            return await _productRepository.GetProductByIdAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProductByNameAsync(string name)
        {
            return await _productRepository.GetProductByNameAsync(name);
        }

        public async Task UpdateAsync(string id, Product product)
        {
            if(product.Id != id)
            {
                throw new Exception();
            }

           await _productRepository.UpdateAsync(product);
        }
    }
}
