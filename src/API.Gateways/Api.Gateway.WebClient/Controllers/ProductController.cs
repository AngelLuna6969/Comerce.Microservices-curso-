using API.Gateway.Models;
using API.Gateway.Models.Catalog.DTOs;
using API.Gateway.Proxies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Gateway.WebClient.Controllers
{
    [Route("products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ICatalogProxy _catalogProxy;

        public ProductController(ICatalogProxy catalogProxy)
        {
            _catalogProxy = catalogProxy;
        }

        [HttpGet]
        public async Task<DataCollection<ProductDto>> GetAll(int page, int take)
        {
            return await _catalogProxy.GetAllAsync(page, take);
        }

        [HttpGet("{id}")]
        public async Task<ProductDto> Get(int id)
        {
            return await _catalogProxy.GetAsync(id);
        }
    }
}
