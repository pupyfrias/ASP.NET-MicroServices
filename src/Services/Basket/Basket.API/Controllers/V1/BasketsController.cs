using Asp.Versioning;
using Basket.API.Entities.V1;
using Basket.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Basket.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketsController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<ShoppingCar>> GetBasketAsync(string userName)
        {
            var shoppingCar = await _basketService.GetBasketAsync(userName);
            return Ok(shoppingCar);
        }

        [HttpDelete("{userName}")]
        public async Task<ActionResult> DeleteBasketAsync(string userName)
        {
            await _basketService.DeleteBasketAsync(userName);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> UpdateBasketAsync(ShoppingCar shoppingCar)
        {
            await _basketService.UpdateBasketAsync(shoppingCar);
            return NoContent();
        }
    }
}
