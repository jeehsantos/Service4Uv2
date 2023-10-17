using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Data;
using ServiceApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using ServiceApp.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ServiceApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IWorkUnit _workUnit;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public UsersController(IWorkUnit workUnit)
        {
            _workUnit = workUnit;
            
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View(_workUnit.User.GetAll());
            //var claimsIdentity = (ClaimsIdentity)this.User.Identity;
            //var currentUser = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            //return View(_workUnit.User.GetAll(u => u.Id != currentUser.Value));
        }

        [HttpGet]
        public IActionResult Block(string id) 
        {
            if(id == null)
            {
                return NotFound();
            }
          
            _workUnit.User.BlockUser(id);
           
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Unblock(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _workUnit.User.UnblockUser(id);
            return RedirectToAction(nameof(Index));
        }
    }
}