using Microsoft.AspNetCore.Mvc;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Data;
using ServiceApp.Models;

namespace ServiceApp.Areas.Client.Controllers
{
    [Area("Client")]
    public class OrchardsController : Controller
    {
        private readonly IWorkUnit _workUnit;
        private readonly ApplicationDbContext _context;


        public OrchardsController(IWorkUnit workUnit, ApplicationDbContext context)
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
        public IActionResult Details(int id)
        {
            var EmployeeDb = _workUnit.Employee.Get(id);
            return View(EmployeeDb);
        }

        [HttpGet]
        public IActionResult EmployeeDetails(int id)
        {
            Employee employee = new Employee();
            employee = _workUnit.Employee.Get(id);
            if (employee == null)
            {
                return NotFound();
            }
            //In JS it will make fields disabled
            return View(employee);
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            Contractor contractor = new Contractor();
            contractor = _workUnit.Contractor.Get(id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }


     

        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _workUnit.Employee.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objCont = _workUnit.Contractor.Get(id);
            if (objCont == null)
            {
                return Json(new { success = false, message = "Error to delete" });
            }

            _workUnit.Contractor.Remove(objCont);
            _workUnit.Save();
            return Json(new { success = true, message = "Contractor deleted !" });

        }
        #endregion
    }
}
