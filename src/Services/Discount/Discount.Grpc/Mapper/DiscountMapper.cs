using AutoMapper;
using Discount.Grpc.Entities.V1;
using Discount.Grpc.Protos;

namespace Discount.Grpc.Mapper
{
    public class DiscountMapper: Profile
    {
        public DiscountMapper()
        {
            CreateMap<Coupon, CreateCouponProtoRequest>().ReverseMap();
            CreateMap<Coupon, UpdateCouponProtoRequest>().ReverseMap();
            CreateMap<Coupon, CouponProtoResponse>().ReverseMap();
        }
    }
}
