using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly UserManager<ApplicationUser> _userManager;



        public ContractorsController(IWorkUnit workUnit, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
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
        public IActionResult Edit(int id) 
        {
            Contractor contractor = new Contractor();
            contractor = _workUnit.Contractor.Get(id);
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
        public async Task<IActionResult> Delete(int id)
        {
            //Contractor
            var objCont = _workUnit.Contractor.Get(id);

            //User login
            var login = await _userManager.FindByIdAsync(objCont.PKID);

            //User in roles
            var role = await _context.UserRoles.FirstOrDefaultAsync(u => u.UserId == login.Id);

            if (objCont == null && login == null && role == null)
            {
                return Json(new { success = false, message = "Error to delete" });
            }
          
            //Removes contractor
            _workUnit.Contractor.Remove(objCont);
            _workUnit.Save();

            //Removes login
            _userManager.DeleteAsync(login);

            //Removes role
            _context.UserRoles.Remove(role);
            _context.SaveChanges();

            return Json(new { success = true, message = "Contractor deleted !" });

        }
        #endregion
    }
}
