using Customer.Persistence.Database;
using Customer.Service.Queries.DTO;
using Service.Common.Collection;
using Microsoft.EntityFrameworkCore;
using Service.Common.Paging;
using Service.Common.Mapping;

namespace Customer.Service.Queries
{
    public interface IClientQueryService
    {
        Task<DataCollection<ClientDTO>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);
        Task<ClientDTO> GetAsync(int id);
    }
    public class ClientQueryService : IClientQueryService
    {
        private readonly ApplicationDbContext _context;
        public ClientQueryService(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<DataCollection<ClientDTO>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var collection = await _context.Clients
                .Where(x => clients == null || clients.Contains(x.ClientId))
                .OrderBy(x => x.Name)
                .GetPagedAsync(page, take);
            return collection.Mapto<DataCollection<ClientDTO>>();
        }

        public async Task<ClientDTO> GetAsync(int id)
        {
            return (await _context.Clients.SingleAsync(x => x.ClientId == id)).Mapto<ClientDTO>();
        }
    }
}
