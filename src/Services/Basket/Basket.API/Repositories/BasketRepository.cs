using Basket.API.Entities;
using Basket.API.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;


namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _cache;

        public BasketRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task DeleteBasketAsync(string userName)
        {
            await _cache.RemoveAsync(userName);
        }

        public async Task<string?> GetBasketAsync(string userName)
        {
            return await _cache.GetStringAsync(userName);

        }

        public async Task UpdateBasketAsync(string userName,  string shoppingCar)
        {
            await _cache.SetStringAsync(userName, shoppingCar);
        }
    }
}
