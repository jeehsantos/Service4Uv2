using AppCore.Data;
using AppCore.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AppCore.Controllers
{
	public class HomeController : Controller
	{
		private readonly ApplicationDbContext _context;

		public HomeController(ApplicationDbContext context)
		{
			 _context = context;
		}
		[HttpGet]
		public async Task<IActionResult> Index()
		{
			return View(await _context.Employee.ToListAsync());
		}

        [HttpGet]
        public IActionResult Create()
        {
			return View();
        }

        [HttpPost]
		[ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Employees employee)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
				{
				employee.CreatedDate = DateTime.Now;
                _context.Employee.Add(employee);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}

			return View();
        }

		[HttpGet]
		public IActionResult Edit(int? id)
		{
			if(id == null)
			{
				return NotFound();
			}
			var contact = _context.Employee.Find(id);
			if (contact == null) {
				return NotFound();
			}

			return View(contact);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Employees employee)
        {
            //var errors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                _context.Employee.Update(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

		[HttpGet]
		public IActionResult Detail(int? id)
		{
			if(id == null)
			{
				return NotFound();
			}
			var contact = _context.Employee.Find(id);
			if (contact == null) {
				return NotFound();
			}

			return View(contact);
		}

        [HttpGet, ActionName("Detail")]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contact = _context.Employee.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var contact = _context.Employee.Find(id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {

			var contact = await _context.Employee.FindAsync(id);
            if (contact == null)
            {
				return View();
            }

            _context.Employee.Remove(contact);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
        }
        public IActionResult Privacy()
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