using ServiceApp.Data.Data.Repository;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.WebPages;

namespace ServiceApp.Data.Data.Repository
{
    internal class EmployeeRepository : Repository<Employee>, IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetListEmployees()
        {
            return _context.Employees.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.EmployeeID.ToString()
            }
            );
        }
        public IEnumerable<SelectListItem> GetListSuburbs()
        {
            return _context.Suburbs.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.SuburbID.ToString(),

            }
            );
        }

        public IEnumerable<SelectListItem> GetListCities()
        {
            return _context.Cities.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.CityID.ToString(),

            }
            );
        }


        public Employee GetEmployee(string userId)
        {
            return _context.Employees.FirstOrDefault(i => i.PKID == userId);
        }

        public void Update(Employee employee)
        {
            var objEmp = _context.Employees.FirstOrDefault(s => s.EmployeeID == employee.EmployeeID);
            objEmp.EmployeeID = employee.EmployeeID;
            objEmp.Name = employee.Name;
            objEmp.Phone = employee.Phone;
            objEmp.Email = employee.Email;
            objEmp.About =  employee.About.IsEmpty() ? "" : employee.About;
            objEmp.SuburbID = employee.SuburbID;
            objEmp.CityID = employee.CityID;
            objEmp.CountryID = employee.CountryID;
            objEmp.DateOfBirth = employee.DateOfBirth;
            objEmp.Language = employee.Language;
            objEmp.Review = employee.Review; 
            objEmp.PKID = employee.PKID;
            objEmp.Active = employee.Active;
            objEmp.NationalityID = employee.NationalityID;
            objEmp.Available = employee.Available;
            _context.SaveChanges();
        }
    }
}
