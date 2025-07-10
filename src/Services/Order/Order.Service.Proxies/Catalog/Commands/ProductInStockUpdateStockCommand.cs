using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Order.Service.Proxies.Catalog.Commands
{
    public class ProductInStockUpdateStockCommand
    {
        public IEnumerable<ProductInStockUpdateItem> Items { get; set; } = new List<ProductInStockUpdateItem>();
    }

    public class ProductInStockUpdateItem
    {
        public int ProductId { get; set; }

        public int Stock {  get; set; }
        public ProductInStockAction Action { get; set; }
    }

    public enum ProductInStockAction
    {
        Add,
        Substract
    }
}
