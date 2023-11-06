using Basket.API.Entities.V1;

namespace Basket.API.Interfaces
{
    public interface IBasketService
    {
        Task<ShoppingCar> GetBasketAsync(string userName);
        Task UpdateBasketAsync(ShoppingCar shoppingCar);
        Task DeleteBasketAsync(string userName);
    }
}