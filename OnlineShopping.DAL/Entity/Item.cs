using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DAL.Entity
{
    [Table("Item")]
    public class Item
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";

        [Required]
        public string Descrption { get; set; } = "";

        [Required]
        public double price { get; set; }

        public double discount { get; set; }

        public string ImageName { get; set; } = "";

        public int SubCategoryId { get; set; }
        public SubCategory SubCategory { get; set; }


        public int? Count { get; set; }

    }
}
