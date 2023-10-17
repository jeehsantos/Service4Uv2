using System.ComponentModel.DataAnnotations;

namespace AppCore.Models
{
	public class Employees
	{
		[Key]
        public int Id { get; set; }
		[Required(ErrorMessage = "You must insert your name")]
        public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		[Required]
		public int Phone { get; set; }
		[Required]
		public string Email { get; set; }
		[Required]
		public string Nationality { get; set; }
		[Required]
		public string About { get; set; }
		[Required]
		public string Suburb { get; set; }

		public bool Available { get; set; }
         
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
	}
}
