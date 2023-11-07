using Discount.API.Entities.V1;

namespace Discount.API.Interfaces
{
    public interface IDiscountRepository
    {
        Task<IEnumerable<Coupon>> GetAllAsync();
        Task<Coupon?> GetByProductNameAsync(string productName);
        Task CreateAsync(Coupon coupon);
        Task UpdateAsync(Coupon coupon);
        Task DeleteAsync(Coupon coupon);
    }
}