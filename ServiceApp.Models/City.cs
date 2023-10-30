using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Models
{
    public class City
    {
        [Key]
        public int CityID { get; set; }
        public string Name { get; set; }

        public string? CountryID { get; set; }

        [ForeignKey("CountryID")]
        public Country Country { get; set; }

        public ICollection<Suburb> Suburbs { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}
