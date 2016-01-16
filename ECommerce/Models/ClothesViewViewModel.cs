using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class ClothesViewViewModel
    {
        public IPagedList<ClothesViewModel> ClothesViewModel { get; set; }
        public Guid? FilterCategory { get; set; }
        public Guid? FilterBrand { get; set; }
        public int? MinPrice { get; set; }
        public int? MaxPrice { get; set; }
    }
}