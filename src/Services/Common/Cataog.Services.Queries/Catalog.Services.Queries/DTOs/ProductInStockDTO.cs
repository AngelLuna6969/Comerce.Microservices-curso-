﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Domain
{
    public class ProductInStockDTO
    {
        public int IDProductInStock { get; set; }
        public int ProductID { get; set; }
        public int Stock {  get; set; }
    }
}
