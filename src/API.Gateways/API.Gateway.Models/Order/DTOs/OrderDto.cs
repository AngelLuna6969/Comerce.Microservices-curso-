﻿using API.Gateway.Models.Catalog.DTOs;
using API.Gateway.Models.Customer.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static API.Gateway.Models.Order.Commons.Emuns;

namespace API.Gateway.Models.Order.DTOs
{
    public class OrderDto
    {
        public int OrderId { get; set; }
        public ClientDto Client { get; set; }
        public string OrderNumber { get; set; }
        public OrderStatus Status { get; set; }
        public OrderPayment PaymentType { get; set; }
        public int ClientId { get; set; }
        public IEnumerable<OrderDetailDto> Items { get; set; } = new List<OrderDetailDto>();
        public DateTime CreatedAt { get; set; }
        public decimal Total {  get; set; }
    }

    public class OrderDetailDto
    {
        public ProductDto Product { get; set; }
        public int OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total { get; set; }
    }
}
