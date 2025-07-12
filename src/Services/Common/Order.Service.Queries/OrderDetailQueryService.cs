using Microsoft.EntityFrameworkCore;
using Order.Persistence.Database;
using Order.Service.Queries.DTOs;
using Service.Common.Collection;
using Service.Common.Mapping;
using Service.Common.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Service.Queries
{
    public interface IOrderDetailQueryService
    {
        Task<DataCollection<OrderDetailDTO>> GetAllAsync(int page, int take, IEnumerable<int> clients = null);

        Task<OrderDetailDTO> GetAsync(int id);
    }
    public class OrderDetailQueryService
    {
        private readonly ApplicationDbContext _context;

        public OrderDetailQueryService(ApplicationDbContext context) { _context = context; }

        public async Task<DataCollection<OrderDetailDTO>> GetAllAsync(int page, int take, IEnumerable<int> clients = null)
        {
            var collection = await _context.OrderDetail
                .Where(x => clients == null || clients.Contains(x.OrderDetailId))
                //.OrderBy(x => x.Name)
                .GetPagedAsync(page, take);

            return collection.Mapto<DataCollection<OrderDetailDTO>>();
        }

        public async Task<OrderDetailDTO> GetAsync(int id)
        {
            return (await _context.OrderDetail.SingleAsync(x => x.OrderDetailId == id)).Mapto<OrderDetailDTO>();
        }
    }
}
