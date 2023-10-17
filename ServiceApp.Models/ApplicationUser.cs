using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required(ErrorMessage = "Insert your name")]
        public string Name { get; set; }

         
        public string? City { get; set; }

         
        public string? Country { get; set; }
    }
}
