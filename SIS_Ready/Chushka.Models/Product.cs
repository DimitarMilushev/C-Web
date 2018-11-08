﻿
using Chushka.Models.Enums;
using System.Collections.Generic;
using System.Text;

namespace Chushka.Models
{
    public class Product
    {
        public Product()
        {
            this.Orders = new HashSet<Order>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Description { get; set; }

        public Type Type { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
