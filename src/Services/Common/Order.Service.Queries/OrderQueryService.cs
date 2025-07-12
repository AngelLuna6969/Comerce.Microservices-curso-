using Microsoft.EntityFrameworkCore;
using Order.Persistence.Database;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;

namespace Order.Service.Queries
{
    public interface IOrderQueryService
    {
        Task<DataCollection<OrderDTO>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);

        Task<OrderDTO> GetAsync(int id);
    }
    public class OrderQueryService : IOrderQueryService
    {
        private readonly ApplicationDbContext _context;

        public OrderQueryService(ApplicationDbContext context) { _context = context; }

        public async Task<DataCollection<OrderDTO>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var collection = await _context.Orders
                .Where(x => clients == null || clients.Contains(x.OrderId))
                //.OrderBy(x => x.Name)
                .GetPagedAsync(page, take);

            return collection.Mapto<DataCollection<OrderDTO>>();
        }

        public async Task<OrderDTO> GetAsync(int id)
        {
            return (await _context.Orders.SingleAsync(x => x.OrderId == id)).Mapto<OrderDTO>();
        }
    }
}
