using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Models
{
    public class Suburb
    {
        [Key]
        public int SuburbID { get; set; }
        public string Name { get; set; }

        [ForeignKey("CityID")]
        public int? CityID { get; set; }
        public City City { get; set; }
    }
}
