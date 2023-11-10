using AutoMapper;
using Basket.API.Entities.V1;
using EventBus.Messages.Event;

namespace Basket.API.Mappings
{
    public class BacketCheckoutMapper: Profile
    {
        public BacketCheckoutMapper()
        {
            CreateMap<BasketCheckout, BasketCheckoutEvent>();  
        }
    }
}
