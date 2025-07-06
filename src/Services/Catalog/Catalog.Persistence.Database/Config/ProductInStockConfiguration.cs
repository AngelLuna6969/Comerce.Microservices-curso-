using Catalog.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Persistence.Database.Config
{
    public class ProductInStockConfiguration
    {
        public ProductInStockConfiguration(EntityTypeBuilder<ProductInStock> entityBuilder)
        {
            //Establece que campo sera la PK
            entityBuilder.HasKey(x => x.IDProductInStock);
            

            //Seeds
            //Products by default
            var products = new List<ProductInStock>();
            var ramdom = new Random();
            for (var i = 1; 1 <= 100; i++)
            {
                products.Add(new ProductInStock
                {
                    IDProductInStock = i,
                    ProductID = i,
                    Stock = ramdom.Next(0, 20),
                });
            }
            entityBuilder.HasData(products);
        }
    }
}
