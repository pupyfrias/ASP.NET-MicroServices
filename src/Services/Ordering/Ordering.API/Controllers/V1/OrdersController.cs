using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Features.Orders.Commands.CheckoutOrderCommand;
using Ordering.Application.Features.Orders.Commands.UpdateOrder;
using Ordering.Application.Features.Orders.Queries.GetOrdersList;

namespace Ordering.API.Controllers.V1
{
    [ApiVersion("1.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public OrdersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{userName}")]
        public async Task<ActionResult<List<OrderVm>>> GetOrderByUserName(string userName)
        {
            var query = new GetOrdersListQuery(userName);
            var orderList = await _mediator.Send(query);
            return Ok(orderList);
        }

        [HttpPost]
        public async Task<ActionResult<int>> CheckoutOrder(CheckoutOrderCommand command)
        {
            var orderNumber = await _mediator.Send(command);
            return Ok(orderNumber);
        }

        [HttpPut]
        public async Task<ActionResult> UpdateOrder(UpdateOrderCommand command)
        {
            await _mediator.Send(command);
            return NoContent();
        }
    }   
}
