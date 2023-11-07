using Discount.Grpc.Entities.V1;

namespace Discount.Grpc.Interfaces
{
    public interface IDiscountService
    {
        Task<IEnumerable<Coupon>> GetAllAsync();
        Task<Coupon?> GetByProductNameAsync(string productName);
        Task CreateAsync(Coupon coupon);
        Task UpdateAsync(string productName, Coupon coupon);
        Task DeleteAsync(string productName);
    }
}