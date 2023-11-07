using Asp.Versioning;
using Catalog.API.Entities;
using Catalog.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService  productService) 
        {
            _productService = productService;
        }


        [HttpPost]
        public async Task<ActionResult<IEnumerable<Product>>> CreateAsync(Product product)
        {
            await _productService.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = product.Id}, product);
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<ActionResult<IEnumerable<Product>>> DeleteAsync(string id)
        {
            await _productService.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync() 
        {
            var productList = await _productService.GetAllProductsAsync();
            return Ok(productList);
        }


        [HttpGet("{category}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetAllAsync(string category)
        {
            var productList = await _productService.GetProductByCategoryNameAsync(category);
            return Ok(productList);
        }


        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<IEnumerable<Product>>> GetById(string id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return Ok(product);
        }

        [HttpPut("{id:length(24)}")]
        public async Task<ActionResult<IEnumerable<Product>>> UpdateAsync(string id, Product product)
        {
            await _productService.UpdateAsync(id, product);
            return NoContent();
        }
    }
}
