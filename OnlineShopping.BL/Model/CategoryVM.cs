using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using OnlineShopping.DAL.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.BL.Model
{
    public class CategoryVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="This Failed Is Required")]
        [MaxLength(100,ErrorMessage = "Max Length : 100")]
        public string Name { get; set; } = "";

        [ValidateNever]
        public ICollection<SubCategory> SubCategory { get; set; }
    }
}
