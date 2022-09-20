using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.BL.Model
{
	public class ResetPasswordVM
	{

        [Required(ErrorMessage = "This Field Is Required")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "This Field Is Required")]
        [Compare("Password", ErrorMessage = "Password Not Match")]
        public string ConfirmPassword { get; set; } = "";

        public string Email { get; set; } = "";
        public string Token { get; set; } = "";
    }
}
