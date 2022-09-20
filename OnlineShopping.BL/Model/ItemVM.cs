using Microsoft.AspNetCore.Http;
using OnlineShopping.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.BL.Model
{
    public class ItemVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "This Failed Is Required")]
        [MaxLength(100)]
        public string Name { get; set; } = "";

        [Required(ErrorMessage = "This Failed Is Required")]
        public string Descrption { get; set; } = "";

        [Required(ErrorMessage = "This Failed Is Required")]
        public double price { get; set; }

        public double? discount { get; set; }

        public IFormFile? Image { get; set; }
        public string? ImageName { get; set; } = "";

        public int SubCategoryId { get; set; }
        public SubCategory? SubCategory { get; set; }

        public int? Count { get; set; }

        public int ? Quantity { get; set; }
    }
}
