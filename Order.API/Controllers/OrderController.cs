using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Service.EventHandlers.Commands;
using Order.Service.Queries;

namespace Order.API.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderQueryService _orderQueryService;
        private readonly ILogger<OrderController> _logger;
        private readonly IMediator _mediator;

        public OrderController(IOrderQueryService orderQueryService, ILogger<OrderController> logger, IMediator mediator)
        {
            _orderQueryService = orderQueryService;
            _logger = logger;
            _mediator = mediator;
        }

        //[HttpPost("{id}")]
        //public async Task<OrderDto> Get(int id)
        //{
        //    return await _orderQueryService.GetAsync(id);
        //}

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand notification)
        {
            await _mediator.Publish(notification);
            return Ok();
        }
    }
}
