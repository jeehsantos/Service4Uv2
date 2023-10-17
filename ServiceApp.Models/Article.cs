using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Models
{
    public class Article
    {
        [Key]
        public int ArticleID { get; set; }

        [Required(ErrorMessage = "The name is mandatory")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The description is mandatory")]
        public string Description { get; set; }

        [Display(Name = "Created data")]
        public string DataCreated { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image")]
        public string UrlImage { get; set; }

        [Required]
        public int CategoryID { get; set; }

        [ForeignKey("CategoryID")]
        public Category Category { get; set; }

    }
}
