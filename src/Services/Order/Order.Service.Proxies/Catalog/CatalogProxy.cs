using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Order.Service.Proxies.Catalog.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Order.Service.Proxies.Catalog
{
    public interface ICatalogProxy
    {
        Task UpdateStockAsync(ProductInStockUpdateStockCommand command);
    }
    public class CatalogProxy : ICatalogProxy
    {
        private readonly HttpClient _httpClient;
        private readonly ApisUrls _apisUrls;

        public CatalogProxy(HttpClient httpClient, IOptions<ApisUrls> apiUrls)
        {
            _httpClient = httpClient;
            _apisUrls = apiUrls.Value;
        }

        public async Task UpdateStockAsync(ProductInStockUpdateStockCommand command)
        {
            var content = new StringContent(
                JsonSerializer.Serialize(command),
                Encoding.UTF8,
                "application/json"
            );
            var request = await _httpClient.PutAsync(_apisUrls.CatalogUrl + "stocks", content);
            request.EnsureSuccessStatusCode();
        }
    }
}
