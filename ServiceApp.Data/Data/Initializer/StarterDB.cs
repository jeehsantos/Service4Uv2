using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using ServiceApp.Models;
using ServiceApp.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Data.Data.Initializer
{
    public class StarterDB : IStarterDB
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public StarterDB(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public void Start()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception ex) { }
            if (_context.Roles.Any(r => r.Name == Utilities.Constants.Admin)) return;

            ////Create roles
            _roleManager.CreateAsync(new IdentityRole(Utilities.Constants.Admin)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Utilities.Constants.Contractor)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Utilities.Constants.Employee)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(Utilities.Constants.Writer)).GetAwaiter().GetResult();

            //Create admin user
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                Name = "Admin",

            }, "Admin123!").GetAwaiter().GetResult();

            ApplicationUser userAdmin = _context.ApplicationUser.Where(us => us.Email == "admin@admin.com").FirstOrDefault();
            _userManager.AddToRoleAsync(userAdmin, Utilities.Constants.Admin).GetAwaiter().GetResult();

            //Create admin user
            _userManager.CreateAsync(new ApplicationUser
            {
                UserName = "writer@admin.com",
                Email = "writer@admin.com",
                EmailConfirmed = true,
                Name = "Writer",

            }, "Writer123!").GetAwaiter().GetResult();

            ApplicationUser userWriter = _context.ApplicationUser.Where(us => us.Email == "writer@admin.com").FirstOrDefault();
            _userManager.AddToRoleAsync(userWriter, Utilities.Constants.Admin).GetAwaiter().GetResult();
        }
    }
}
