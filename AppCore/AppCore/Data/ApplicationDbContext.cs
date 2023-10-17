using AppCore.Models;
using Microsoft.EntityFrameworkCore;

namespace AppCore.Data
{
	public class ApplicationDbContext : DbContext
	{
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> optons) : base(optons)  

        {
               
        }

        //Add models
        public DbSet<Employees> Employee { get; set; }
    }
}
