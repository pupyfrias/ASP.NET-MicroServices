
using AutoMapper;
using Discount.Grpc.Entities.V1;
using Discount.Grpc.Interfaces;
using Discount.Grpc.Protos;
using Grpc.Core;

namespace Discount.Grpc.Services
{
    public class DiscountService: DiscountProtoService.DiscountProtoServiceBase
    {

        private readonly IDiscountRepository _repository;
        private readonly IMapper _mapper;

        public DiscountService(IDiscountRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }


        public override async Task<Empty> Update(UpdateCouponProtoRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);

            await _repository.UpdateAsync(coupon);
            return new Empty();
        }

        public override async Task<Empty> Create(CreateCouponProtoRequest request, ServerCallContext context)
        {
            var coupon = _mapper.Map<Coupon>(request);
            await _repository.CreateAsync(coupon);
            return new Empty();
        }

        public override async Task<Empty> Delete(ProductProtoRequest request, ServerCallContext context)
        {
            var coupon = await _repository.GetByProductNameAsync(request.ProductName);

            if (coupon == null)
            {
                throw new Exception($"Discount of {request.ProductName} not found");
            }
            await _repository.DeleteAsync(coupon);
            return new Empty();
        }

        public override async Task<CouponProtoList> GetAll(Empty request, ServerCallContext context)
        {
            var couponList = await _repository.GetAllAsync();
            return _mapper.Map<CouponProtoList>(couponList);
        }

        public override async  Task<CouponProtoResponse> GetByProductName(ProductProtoRequest request, ServerCallContext context)
        {

            var coupon = await _repository.GetByProductNameAsync(request.ProductName);
            return _mapper.Map<CouponProtoResponse>(coupon);
        }
    }
}
