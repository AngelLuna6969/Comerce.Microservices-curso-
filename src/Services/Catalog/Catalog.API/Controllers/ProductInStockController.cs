using Catalog.Domain;
using Catalog.Services.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Common.Collection;

namespace Catalog.API.Controllers
{
    [ApiController]
    [Route("stocks")]
    public class ProductInStockController : ControllerBase
    {
        private readonly IProductInStockQueryService _productInStockQueryService;

        public ProductInStockController(IProductInStockQueryService productinstockQueryService)
        {
            _productInStockQueryService = productinstockQueryService;
        }

        [HttpGet]
        public async Task<DataCollection<ProductInStockDTO>> GetAll(int page = 1, int take = 10, string ids = null)
        {
            IEnumerable<int> products = null;
            if (!string.IsNullOrEmpty(ids))
            {
                products = ids.Split(',').Select(x => Convert.ToInt32(x));
            }
            return await _productInStockQueryService.GetAllAsync(page, take, products);

        }
        
    }
}
