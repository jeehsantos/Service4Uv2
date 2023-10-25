using Microsoft.AspNetCore.Mvc;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Data;
using ServiceApp.Models;
using Microsoft.AspNetCore.Identity;

namespace ServiceApp.Areas.Client.Controllers
{
    [Area("Client")]
    public class OrchardsController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IWorkUnit _workUnit;
        private readonly ApplicationDbContext _context;


        public OrchardsController(IWorkUnit workUnit, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
            var contractor = _workUnit.Contractor.GetContractor(id);
            if (contractor != null)
            {
                return View(contractor);
            }
            return Index();
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

        //edit profile
        [HttpGet]
        public IActionResult Edit()
        {
            var id = _userManager.GetUserId(HttpContext.User);

            Contractor contractor = new Contractor();
            contractor = _workUnit.Contractor.GetContractor(id);
            if (contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }
         
        //edit profile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                _workUnit.Contractor.Update(contractor);
                _workUnit.Save();
                return View(contractor);
            }

            return View(contractor);
        }


        #region
        [HttpGet]
        public IActionResult GetAll()
        { 
            return Json(new { data = _workUnit.Employee.GetAll(u => u.Active && u.Available) });
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
