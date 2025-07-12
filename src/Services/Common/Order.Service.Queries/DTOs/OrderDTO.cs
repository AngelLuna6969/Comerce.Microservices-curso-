using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Order.Common.Enum;

namespace Order.Service.Queries.DTOs
{
    public class OrderDTO
    {
        public int OrderId { get; set; }
        public OrderStatus Status { get; set; }
        public OrderPayment PaymentType { get; set; }
        public int ClientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal Total { get; set; }

        public ICollection<OrderDetailDTO> Items { get; set; } = new List<OrderDetailDTO>();

    }
}
