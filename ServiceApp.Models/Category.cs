using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Models
{
    public class Category
    {
        [Key]
        public int CategoryID { get; set; }
        [Required(ErrorMessage = "Insert a category name")]
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }

        [Display(Name = "View sequence")]
        public int? Sort { get; set; }
    }
}
