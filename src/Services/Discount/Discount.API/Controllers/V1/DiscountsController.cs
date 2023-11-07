using Asp.Versioning;
using Discount.API.Entities.V1;
using Discount.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Discount.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class DiscountsController : ControllerBase
    {
        private readonly IDiscountService _discountService;

        public DiscountsController(IDiscountService discountService)
        {
            _discountService = discountService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Coupon>>> GetAllDiscount()
        {
            var countList = await _discountService.GetAllAsync();
            return Ok(countList);
        }

        [HttpGet("{productName}")]
        public async Task<ActionResult<Coupon?>> GetByProductNameDiscount(string productName)
        {
            var count = await _discountService.GetByProductNameAsync(productName);
            return Ok(count);
        }

        [HttpDelete("{productName}")]
        public async Task<ActionResult> DeleteDiscount(string productName)
        {
            await _discountService.DeleteAsync(productName);
            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult> CreateDiscount(Coupon coupon)
        {
            await _discountService.CreateAsync(coupon);
            return CreatedAtAction(nameof(GetByProductNameDiscount), new { productName = coupon.ProductName }, coupon);
        }


        [HttpPut("{productName}")]
        public async Task<ActionResult> UpdateDiscount(string productName, Coupon coupon)
        {
            await _discountService.UpdateAsync(productName, coupon);
            return NoContent();
        }
    }
}
