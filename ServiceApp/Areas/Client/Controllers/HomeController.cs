using Microsoft.AspNetCore.Mvc;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Data;
using ServiceApp.Models;
using System.Diagnostics;
using ServiceApp.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using ServiceApp.Utilities;
using MailKit.Net.Smtp;
using MimeKit;
using MailKit.Security;
using static System.Net.Mime.MediaTypeNames;

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
                    // Change to Employees controler
                    return RedirectToAction("Index", "Employees");

                }
                else if(User.IsInRole(Constants.Contractor)){
                    //Remains in the Home controler
                    return View();

                }
            }
            return View();

        }
        //[HttpPost]
        //public IActionResult Index()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult Articles()
        {
            HomeVM homeVM = new HomeVM()
            {
                ListArticles = _workUnit.Article.GetAll()
            };
            return View(homeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Articles(string )
        {
            
            //Para envío de correo
            var mensaje = new MimeMessage();
            mensaje.From.Add(new MailboxAddress("Test envio email", "idreamzjsm@gmail.com"));
            mensaje.To.Add(new MailboxAddress("Test enviado", "idreamzjsm@gmail.com"));
            mensaje.Subject = "Test email asp.net core";
            mensaje.Body = new TextPart("plain")
            {
                Text = "Hola saludo desde asp.net core"
            };

            using (var cliente = new SmtpClient())
            {
                cliente.Connect("smtp.gmail.com", 465);
                cliente.Authenticate("jefferson.macedojsm@gmail.com", "fxjm aqev eujc nbmy");
                cliente.Send(mensaje);
                cliente.Disconnect(true);
            }
            return View(await _context.Usuario.ToListAsync());
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