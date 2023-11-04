﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }

        //Use as Key later
        public string PKID { get; set; }

        [Display(Name = "Name")]
        [Required(ErrorMessage = "Insert your name")]
        public string Name { get; set; }

        [Display(Name = "Phone")]
        [Required(ErrorMessage = "Insert your phone number")]
        public int Phone { get; set; }

        [Display(Name = "Email")]
        public string Email { get; set; }

        [Display(Name = "About you")]
        [Required(ErrorMessage = "What type of job?")]
        public string About { get; set; }

        [Display(Name = "Date of birth")]
        public string DateOfBirth { get; set; }

        [Display(Name= "Nationality")]
        public string? NationalityID { get; set; }


        [Display(Name = "Prefered language")]
        public string Language { get; set; }
        public int Review { get; set; }
 
        public bool Available { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }


        [ForeignKey("CountryID")]
        public Country Country { get; set; }
        [Display(Name = "Country")]
        public string? CountryID { get; set; }
        ////COD01
        [ForeignKey("CityID")]
        public City City { get; set; }

        [Display(Name = "City")]
         public int? CityID { get; set; }

        
        [ForeignKey("SuburbID")]
        public Suburb Suburb { get; set; }
        [Display(Name = "SuburbID")]
        public int? SuburbID { get; set; }

        //[ForeignKey("SuburbID")]
        //public IEnumerable<SelectListItem> ListSuburbs { get; set; }
        //[ForeignKey("CityID")]
        //public IEnumerable<SelectListItem> ListCities { get; set; }

    }
}
