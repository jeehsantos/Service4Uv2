using Microsoft.AspNetCore.Mvc;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Data;
using ServiceApp.Models;

namespace ServiceApp.Areas.Client.Controllers
{

    [Area("Client")]
    public class EmployeesController : Controller
    {

        private readonly IWorkUnit _workUnit;
        private readonly ApplicationDbContext _context;


        public EmployeesController(IWorkUnit workUnit, ApplicationDbContext context)
        {
            _workUnit = workUnit;
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
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

        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _workUnit.Employee.GetAll() });
        }

        #endregion
    }
}
