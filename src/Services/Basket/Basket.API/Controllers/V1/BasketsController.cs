using Asp.Versioning;
using AutoMapper;
using Basket.API.Entities.V1;
using Basket.API.Interfaces;
using EventBus.Messages.Event;
using MassTransit;
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
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;

        public BasketsController(IBasketService basketService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _basketService = basketService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
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

        [HttpPost("checkout")]
        public async Task<ActionResult> CheckoutAsync(BasketCheckout basketCheckout)
        {
            var basket = await _basketService.GetBasketAsync(basketCheckout.UserName);

            if(basket == null)
            {
                return BadRequest();
            }
            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);

            await _publishEndpoint.Publish(eventMessage);

            await _basketService.DeleteBasketAsync(basketCheckout.UserName);
            return Accepted();
        }

    }
}
