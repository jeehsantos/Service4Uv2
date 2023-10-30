using Microsoft.AspNetCore.Mvc;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Data;
using ServiceApp.Models;
using Microsoft.AspNetCore.Identity;
using ServiceApp.Models.ViewModels;

namespace ServiceApp.Areas.Client.Controllers
{

    [Area("Client")]
    public class EmployeesController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWorkUnit _workUnit;
        private readonly ApplicationDbContext _context;


        public EmployeesController(IWorkUnit workUnit, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _workUnit = workUnit;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var id = _userManager.GetUserId(HttpContext.User);
            return Index(id);
        }

        [HttpPost]
        public IActionResult Index(string id)
        { 
            SuburbVM suburbVM = new SuburbVM()
            {

                Employee = _workUnit.Employee.GetEmployee(id) ,
                ListSuburbs = _workUnit.Suburb.GetListSuburb(),
                ListCities = _workUnit.City.GetListCities(),
                ListCountries = _workUnit.Country.GetListCountries()


            };
            
            return View(suburbVM);
        }
         
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _workUnit.Employee.Update(employee);
                _workUnit.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(employee);
        }
         
    }
}
