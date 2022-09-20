using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.BL.Model
{
	public class LoginVM
	{
        [Required(ErrorMessage = "This Field Is Required")]
        public string Data { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]

        public string Password { get; set; } = "";

       
    }
}
