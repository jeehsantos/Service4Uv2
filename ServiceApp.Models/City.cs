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

        [ForeignKey("CountryID")]
        public string? CountryID { get; set; }
        public Country Country { get; set; }

        public List<Suburb> Suburbs { get; set; }
    }
}
