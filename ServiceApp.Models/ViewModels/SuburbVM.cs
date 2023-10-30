using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace ServiceApp.Models.ViewModels
{
    public class SuburbVM
    {
        public Employee Employee { get; set; }
        public Contractor Contractor { get; set; }

        public IEnumerable<SelectListItem>  ListSuburbs { get; set; }
        public IEnumerable<SelectListItem> ListCities { get; set; }
        public IEnumerable<SelectListItem> ListCountries { get; set; }



    }
}
