﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using ServiceApp.Data;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Models;
using ServiceApp.Models.ViewModels;

namespace ServiceApp.Areas.Admin.Controllers
{
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class EmployeesController : Controller
    {
        private readonly IWorkUnit _workUnit;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
       
        public EmployeesController(IWorkUnit workUnit, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _workUnit = workUnit;
            _context = context;
            _userManager = userManager;
         
    }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            SuburbVM suburbVM = new SuburbVM()
            {
                Employee = new ServiceApp.Models.Employee(),
                ListSuburbs = _workUnit.Suburb.GetListSuburb(),
                ListCities = _workUnit.City.GetListCities(),
                ListCountries = _workUnit.Country.GetListCountries()


            };
            return View(suburbVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                employee.DateCreated = DateTime.Now;
                _workUnit.Employee.Add(employee);
                _workUnit.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Employee employee = new Employee();
            employee = _workUnit.Employee.Get(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        { 
                _workUnit.Employee.Update(employee);
                _workUnit.Save();
                return RedirectToAction(nameof(Index));
          
        }


        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _workUnit.Employee.GetAll() });
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            //Employee
            var objEmp = _workUnit.Employee.Get(id);
            //User login
            var login = await _userManager.FindByIdAsync(objEmp.PKID);
            //User in roles
            var role = await _context.UserRoles.FirstOrDefaultAsync(u => u.UserId == login.Id);

            if (objEmp == null && login == null && role == null)
            {
                return Json(new { success = false, message = "Error to delete" });
            }

           
                _workUnit.Employee.Remove(objEmp);
                _workUnit.Save();
                
            
           
                _userManager.DeleteAsync(login);
         
                _context.UserRoles.Remove(role);
                _context.SaveChanges();
         
            
            
            return Json(new { success = true, message = "Employee deleted !" });

        }
        #endregion
    }
}
