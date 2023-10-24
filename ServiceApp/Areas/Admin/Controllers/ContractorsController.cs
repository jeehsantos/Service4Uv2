using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.Data;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Models;

namespace ServiceApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class ContractorsController : Controller
    {
        private readonly IWorkUnit _workUnit;
        private readonly ApplicationDbContext _context;


        public ContractorsController(IWorkUnit workUnit, ApplicationDbContext context)
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
        public IActionResult Create() 
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Contractor contractor)
        {
            if(ModelState.IsValid)
            {
                contractor.DateCreated = DateTime.Now;
                _workUnit.Contractor.Add(contractor);
                _workUnit.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(contractor);
        }

        [HttpGet]
        public IActionResult Edit(string id) 
        {
            Contractor contractor = new Contractor();
            contractor = _workUnit.Contractor.GetContractor(id);
            if(contractor == null)
            {
                return NotFound();
            }

            return View(contractor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Contractor contractor)
        {
            if (ModelState.IsValid)
            {
                _workUnit.Contractor.Update(contractor);
                _workUnit.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(contractor);
        } 

        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _workUnit.Contractor.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objCont = _workUnit.Contractor.Get(id);
            if (objCont == null)
            {
                return Json(new { success = false, message = "Error to delete" });
            }
            //if (objCont.Id == null)
            //{
            //    return Json(new { success = false, message = "Error to delete" });
            //}


            //_workUnit.Contractor.Remove(objCont);
            _workUnit.Save();
            return Json(new { success = true, message = "Contractor deleted !" });

        }
        #endregion
    }
}
