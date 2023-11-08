using Discount.Grpc.Protos;

namespace Basket.API.Services.gRPC
{
    public class DiscountGrpcService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _serviceClient;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient serviceClient)
        {
            _serviceClient = serviceClient;
        }

        public async Task<CouponProtoResponse> GetDiscountAsync(string productName)
        {
            return await _serviceClient.GetByProductNameAsync(new ProductProtoRequest { ProductName = productName });
        }
    }
}
