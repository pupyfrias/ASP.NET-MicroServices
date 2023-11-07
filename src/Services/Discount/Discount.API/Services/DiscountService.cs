using Discount.API.Entities.V1;
using Discount.API.Interfaces;

namespace Discount.API.Services
{
    public class DiscountService : IDiscountService
    {
        private readonly IDiscountRepository _repository;

        public DiscountService(IDiscountRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateAsync(Coupon coupon)
        {
            await _repository.CreateAsync(coupon);
        }

        public async Task DeleteAsync(string productName)
        {
            var coupon = await _repository.GetByProductNameAsync(productName);

            if(coupon == null)
            {
                throw new Exception($"Discount of {productName} not found");
            }
            await _repository.DeleteAsync(coupon);
        }

        public Task<IEnumerable<Coupon>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Coupon?> GetByProductNameAsync(string productName)
        {
            return _repository.GetByProductNameAsync(productName);
        }

        public async Task UpdateAsync(string productName , Coupon coupon)
        {
            if(productName != coupon.ProductName)
            {
                throw new Exception($"Id {productName} not match with coupon Id {coupon.ProductName}");
            }

            await _repository.UpdateAsync(coupon);
        }
    }
}
