using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Dapper
{
    public class Clothes
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public Guid CategoryId { get; set; }
        public Guid BrandId { get; set; }
    }
}