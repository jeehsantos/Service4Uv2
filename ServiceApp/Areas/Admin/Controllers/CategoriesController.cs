using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceApp.Data;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Models;


namespace ServiceApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly IWorkUnit _workUnit;
        private readonly ApplicationDbContext _context;


        public CategoriesController(IWorkUnit workUnit, ApplicationDbContext context)
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
        public IActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                _workUnit.Category.Add(category);
                _workUnit.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Category category = new Category();
            category = _workUnit.Category.Get(id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _workUnit.Category.Update(category);
                _workUnit.Save();
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }

        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _workUnit.Category.GetAll() });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var objCont = _workUnit.Category.Get(id);
            if (objCont == null)
            {
                return Json(new { success = false, message = "Error to delete" });
            }

            _workUnit.Category.Remove(objCont);
            _workUnit.Save();
            return Json(new { success = true, message = "Category deleted !" });

        }
        #endregion
    }
}
