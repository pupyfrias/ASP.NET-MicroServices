using Basket.API.Entities.V1;
using Basket.API.Interfaces;
using Basket.API.Services.gRPC;
using Newtonsoft.Json;

namespace Basket.API.Services
{
    public class BasketService : IBasketService
    {

        private readonly IBasketRepository _basketRepository;
        private readonly DiscountGrpcService _discountGrpcService;

        public BasketService(IBasketRepository basketRepository, DiscountGrpcService discountGrpcService)
        {
            _basketRepository = basketRepository;
            _discountGrpcService = discountGrpcService;
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await _basketRepository.DeleteBasketAsync(userName);
        }

        public async Task<ShoppingCar> GetBasketAsync(string userName)
        {
            var basket = await _basketRepository.GetBasketAsync(userName);

            if (string.IsNullOrEmpty(basket))
            {
                return new ShoppingCar(userName);
            }
            else
            {
                return JsonConvert.DeserializeObject<ShoppingCar>(basket)?? new ShoppingCar(userName);
            }
        }

        public async Task UpdateBasketAsync(ShoppingCar shoppingCar)
        {

            foreach (var item in shoppingCar.Items)
            {
               var coupon = await _discountGrpcService.GetDiscountAsync(item.ProductName);
                item.Price -= coupon.Amount;

            }
            string shoppingCarString  = JsonConvert.SerializeObject(shoppingCar);
            await _basketRepository.UpdateBasketAsync(shoppingCar.UserName, shoppingCarString);
        }
    }
}
    