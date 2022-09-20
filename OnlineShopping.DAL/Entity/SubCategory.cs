using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DAL.Entity
{
    [Table("SubCategory")]
    public class SubCategory
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = "";
        

        public string ImageName { get; set; } = "";

        public int CategoryId { get; set; }
        
        public Category Category { get; set; }
        public ICollection<Item> Item { get; set; }

    }
}
