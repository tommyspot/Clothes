using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class ClothesCreateViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        [Display(Name = "Category")]
        public Guid? FilterCategory { get; set; }
        [Display(Name = "Brand")]
        public Guid? FilterBrand { get; set; }
    }
}