﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE
{
    public class Product
    {
        public Product()
        {
            DeleteStatus = false;
        }
        public int id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public bool DeleteStatus { get; set; }
        public List<Invoice> invoices { get; set; } = new List<Invoice>();
        
    }
}
