using API.Gateway.Models;
using API.Gateway.Models.Customer.DTOs;
using API.Gateway.Proxies.Config;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace API.Gateway.Proxies
{
    public interface ICustomerProxy
    {
        Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        Task<ClientDto> GetAsync(int id);
    }
    public class CustomerProxy : ICustomerProxy
    {
        private readonly ApiUrls _apiUrls;
        private readonly HttpClient _httpClient;

        public CustomerProxy(IOptions<ApiUrls> apiUrls, HttpClient httpClient, IHttpContextAccessor httpContextAccessor) 
        {
            httpClient.AddBearerToken(httpContextAccessor);

            _httpClient = httpClient;
            _apiUrls = apiUrls.Value;
        }

        public async Task<DataCollection<ClientDto>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var ids = string.Join(",", clients ?? new List<int>());
            var request = await _httpClient.GetAsync($"{_apiUrls.CustomerUrl}/clients?page={page}&take={take}&ids={ids}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<DataCollection<ClientDto>>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                }
            );
        }

        public async Task<ClientDto> GetAsync(int id)
        {
            var request = await _httpClient.GetAsync($"{_apiUrls.CustomerUrl}/clients/{id}");
            request.EnsureSuccessStatusCode();

            return JsonSerializer.Deserialize<ClientDto>(
                await request.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                }
            );
        }
    }
}
