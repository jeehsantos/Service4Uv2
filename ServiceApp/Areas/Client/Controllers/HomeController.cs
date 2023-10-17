using Microsoft.AspNetCore.Mvc;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Data;
using ServiceApp.Models;
using System.Diagnostics;
using ServiceApp.Models.ViewModels;

namespace ServiceApp.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        private readonly IWorkUnit _workUnit;
         


        public HomeController(IWorkUnit workUnit, ApplicationDbContext context)
        {
            _workUnit = workUnit;
            
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
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


 
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}