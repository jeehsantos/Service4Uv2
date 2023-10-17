using ServiceApp.Data.Data.Repository;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServiceApp.Data.Data.Repository
{
    internal class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public void BlockUser(string IdUser)
        {
            var userDb = _context.ApplicationUser.FirstOrDefault(u => u.Id == IdUser);
            userDb.LockoutEnd = DateTime.Now.AddYears(1);
            var userEmp = _context.Employees.FirstOrDefault(u => u.id == IdUser);
            var userCont = _context.Contractors.FirstOrDefault(u => u.Id == IdUser);

            if (userEmp != null)
            {
                userEmp.Active = false;
            }
            if (userCont != null)
            {
                userCont.Active = false;
            }
            _context.SaveChanges();
        }

        public void UnblockUser(string IdUser)
        {
            var userDb = _context.ApplicationUser.FirstOrDefault(u => u.Id == IdUser);
            userDb.LockoutEnd = DateTime.Now;
            var userEmp = _context.Employees.FirstOrDefault(u => u.id == IdUser);
            var userCont = _context.Contractors.FirstOrDefault(u => u.Id == IdUser);

            if (userEmp != null)
            {
                userEmp.Active = true;
            }
            if (userCont != null)
            {
                userCont.Active = true;
            }
            _context.SaveChanges();
        }


    }
}
