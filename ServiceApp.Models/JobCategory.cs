using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Models
{
    public class JobCategory
    {
        [Key]
        public int JobCategoryID { get; set; }

        [Display(Name = "Name category")]
        [Required(ErrorMessage ="Insert the Category name")]
        public string CategoryName { get; set; }

        [Display(Name ="Type name")]
        [Required(ErrorMessage ="Insert the Category type")]
        public string CategoryType { get; set; }
    }
}
