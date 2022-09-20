using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShopping.DAL.Extend
{
    [Index(nameof(PhoneNumber), IsUnique = true)]
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string Name { get; set; }
    }
}
