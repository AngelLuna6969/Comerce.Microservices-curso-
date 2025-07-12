using API.Gateway.Models;
using API.Gateway.Models.Customer.DTOs;
using API.Gateway.Proxies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("clients")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly ICustomerProxy _customerProxy;

        public ClientController(ICustomerProxy customerProxy)
        {
            _customerProxy = customerProxy;
        }

        [HttpGet]
        public async Task<DataCollection<ClientDto>> GetAll(int page, int take)
        {
            return await _customerProxy.GetAllAsync(page, take);
        }

        [HttpGet("{id}")]
        public async Task<ClientDto> Get(int id)
        {
            return await _customerProxy.GetAsync(id);
        }
    }
}
