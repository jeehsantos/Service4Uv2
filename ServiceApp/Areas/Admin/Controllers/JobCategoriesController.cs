using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.Data;
using ServiceApp.Data.Data.Repository;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Models;

namespace ServiceApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class JobCategoriesController : Controller
    {
        private readonly IWorkUnit _workUnit;
        private readonly ApplicationDbContext _context;

        public JobCategoriesController(IWorkUnit workUnit, ApplicationDbContext context)
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
        public IActionResult Create(JobCategory category)
        {
            if(ModelState.IsValid)
            {
                _workUnit.JobCategory.Add(category);
                _workUnit.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            JobCategory category = new JobCategory();
            category = _workUnit.JobCategory.Get(id);
            if(category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(JobCategory category)
        {
            if (ModelState.IsValid)
            {
                _workUnit.JobCategory.Update(category);
                _workUnit.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        #region
        [HttpGet]
        public IActionResult GetAll()
        { 
            return Json(new {data = _workUnit.JobCategory.GetAll()});
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objCat = _workUnit.JobCategory.Get(id);
            if(objCat == null)
            {
                return Json(new { success = false, message = "Error to delete" });
            }

            _workUnit.JobCategory.Remove(objCat);
            _workUnit.Save();
            return Json(new { success = true, message = "Category deleted !" });

        }
        #endregion
    }
}
