using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Models
{
    public class Contractor
    {
        [Key]
        public int ContractorID { get; set; }

        //Use as PK later
        public string Id { get; set; }


        [Display(Name = "Contractor name")]
        [Required(ErrorMessage = "Insert the contractor name")]
        public string Name { get; set; }
        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Insert your phone")]
        public int Phone { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "Insert your email")]
        public string Email { get; set; }



        ////COD01
        //[Display(Name = "City")]
        //[Required(ErrorMessage = "Insert your city")]
        //public int? CityID { get; set; }
        ////COD01
        //[ForeignKey("CityID")]
        //public City City { get; set; }

        [Display(Name = "Suburb")]
        public int? SuburbID { get; set; }

        [ForeignKey("SuburbID")]
        public Suburb Suburb { get; set; }


        //COD02
        [Display(Name = "Job type")]
        public string JobType { get; set; }

        //COD01
        public int CategoryID { get; set; }

        public bool Active { get; set; }

        [Display(Name = "Supplementary Seasonal Employer")]
        public bool SSEEmployer { get; set; }
        public DateTime DateCreated { get; set; }


        // COD01 - Review this should come as dropdown in a FK
        // COD02 - Review this should come as dropdown in a AFTER IMPLEMENTING FK

    }
}


