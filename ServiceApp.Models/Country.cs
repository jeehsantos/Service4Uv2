using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Models
{
    public class Country
    {
        [Key]
        public string CountryID { get; set; }
        public string Name { get; set; }

        public ICollection<Employee> Employees { get; set; }
        public ICollection<City> Cities { get; set; }

    
    }
}
