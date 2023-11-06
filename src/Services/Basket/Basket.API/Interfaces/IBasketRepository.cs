using Basket.API.Entities;


namespace Basket.API.Interfaces
{
    public interface IBasketRepository
    {
        Task<string?> GetBasketAsync(string userName);
        Task UpdateBasketAsync(string userName, string shoppingCar);
        Task DeleteBasketAsync(string userName);
    }
}
