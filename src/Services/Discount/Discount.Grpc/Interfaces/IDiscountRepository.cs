

using Discount.Grpc.Entities.V1;

namespace Discount.Grpc.Interfaces
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