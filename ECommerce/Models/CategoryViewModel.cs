using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models
{
    public class CategoryViewModel
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "The field is required.")]
        public string Name { get; set; }
    }
}