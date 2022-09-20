using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.BL.Model
{
    public class ForgetPasswordVM
    {
        [Required(ErrorMessage = "This Field Is Required")]
        public string Email { get; set; } = "";  
    }
}
