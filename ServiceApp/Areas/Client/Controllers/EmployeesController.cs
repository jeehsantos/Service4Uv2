using Microsoft.AspNetCore.Mvc;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Data;
using ServiceApp.Models;
using Microsoft.AspNetCore.Identity;

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
            var employee = _workUnit.Employee.GetEmployee(id);
            if (employee != null)
            {
               return View(employee);
            }
            return Index();
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
