using Microsoft.AspNetCore.Mvc;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Data;
using ServiceApp.Models;
using System.Diagnostics;
using ServiceApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using ServiceApp.Utilities;
using Microsoft.CodeAnalysis.Operations;

namespace ServiceApp.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly IWorkUnit _workUnit;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _context;

        public HomeController(IWorkUnit workUnit, ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _workUnit = workUnit;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
             
            var id = _userManager.GetUserId(HttpContext.User);
            if(id != null)
            {
                if (User.IsInRole(Constants.Employee))
                {
                    return Index(id);
                }
            }
            return View();

        }

        [HttpPost]
        public IActionResult Index(string id)
        {
            var employee = _workUnit.Employee.GetEmployee(id);
            if (employee != null)
            {
                ViewBag.EmployeeId = employee.EmployeeID;
                ViewBag.id = employee.id;
                ViewBag.Name = employee.Name;
                ViewBag.Phone = employee.Phone;
                ViewBag.Email = employee.Email;
                TempData["About"] = employee.About;
                ViewBag.SuburbID = employee.SuburbID.ToString();
                ViewBag.DateOfBirth = employee.DateOfBirth;
                ViewBag.NationalityID = employee.NationalityID;
                ViewBag.Country = employee.Country;
                ViewBag.Language = employee.Language;
                ViewBag.Review = employee.Review.ToString();
                ViewBag.Image = employee.Image;
                ViewBag.Available = employee.Available;
                ViewBag.Active = employee.Active;
                ViewBag.DateCreated = employee.DateCreated;

                return View("EmployeeDetails");
            }
            return Index();
        }


        [HttpGet]
        public IActionResult Articles()
        {
            HomeVM homeVM = new HomeVM()
            {
                ListArticles = _workUnit.Article.GetAll()
            };
            return View(homeVM);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var articleDb = _workUnit.Article.Get(id);
            return View(articleDb);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }


        //Remove and use only for Employees
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EmployeeDetails(Employee employee)
        {
            //if (ModelState.IsValid)
            //{
                _workUnit.Employee.Update(employee);
                _workUnit.Save();
                return RedirectToAction(nameof(Index));
            //}

            //return View(employee);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}