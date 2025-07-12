using API.Gateway.Models;
using API.Gateway.Models.Order.Commands;
using API.Gateway.Models.Order.DTOs;
using API.Gateway.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Api.Gateway.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderProxy _orderProxy;
        private readonly ICustomerProxy _customerProxy;
        private readonly ICatalogProxy _catalogProxy;

        public OrderController(
            IOrderProxy orderProxy,
            ICustomerProxy customerProxy, ICatalogProxy catalogProxy)
        {
            _orderProxy = orderProxy;
            _customerProxy = customerProxy;
            _catalogProxy = catalogProxy;
        }
        [HttpGet]
        public async Task<DataCollection<OrderDto>> GetAll(int page, int take)
        {
            var result = await _orderProxy.GetAllAsync(page, take);
            if (result.HasItems)
            {
                var clientsIds = result.Items
                    .Select(x => x.ClientId)
                    .GroupBy(g => g)
                    .Select(x => x.Key).ToList();
                var clients = await _customerProxy.GetAllAsync(1, clientsIds.Count(), clientsIds);
                foreach (var order in result.Items)
                {
                    order.Client = clients.Items.Single(x => x.ClientId == order.ClientId);
                }
            }
            return result;
        }

        //[HttpGet("{id}")]
        //public async Task<OrderDto> Get(int id)
        //{
        //    var result = await _orderProxy.GetAsync(id);

        //    result.Client = await _customerProxy.GetAsync(result.ClientId);

        //    var productIds = result.Items
        //        .Select(x => x.ProductId)
        //        .GroupBy(g => g)
        //        .Select(x => x.Key).ToList();
        //    var products = await _catalogProxy.GetAllAsync(1, productIds.Count(), productIds);
        //    foreach (var item in result.Items)
        //    {
        //        item.Product = products.Items.Single(x => x.IDProduct == item.ProductId);
        //    }

        //    return result;
        //}

        [HttpPost]
        public async Task<IActionResult> Create(OrderCreateCommand command)
        {
            await _orderProxy.CreateAsync(command);
            return Ok();
        }
    }

}
