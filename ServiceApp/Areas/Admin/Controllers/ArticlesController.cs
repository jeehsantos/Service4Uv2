using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Data;
using ServiceApp.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Authorization;

namespace ServiceApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Writer")]
    [Area("Admin")]
    public class ArticlesController : Controller
    {
        private readonly IWorkUnit _workUnit;
        private readonly IWebHostEnvironment _hostingEnvironment;


        public ArticlesController(IWorkUnit workUnit, IWebHostEnvironment hostingEnvironment)
        {
            _workUnit = workUnit;
            _hostingEnvironment = hostingEnvironment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create() 
        {
           ArticleVM articleVM = new ArticleVM()
           {
               Article = new ServiceApp.Models.Article(),
               ListCategories = _workUnit.Category.GetListCategories()
           };

            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                string mainPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;
                if(articleVM.Article.ArticleID == 0)
                {
                    //NEW ARTICLE
                    string Filename = Guid.NewGuid().ToString();
                    var upload = Path.Combine(mainPath, @"img\articles");
                    var extension = Path.GetExtension(files[0].FileName);
              

                    using(var fileStreams = new FileStream(Path.Combine(upload, Filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    articleVM.Article.UrlImage = @"\img\articles\" + Filename + extension;
                    articleVM.Article.DataCreated = DateTime.Now.ToString();

                    _workUnit.Article.Add(articleVM.Article);
                    _workUnit.Save();
                    return RedirectToAction(nameof(Index));
                }
            }
            articleVM.ListCategories = _workUnit.Category.GetListCategories();
            return View(articleVM);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            ArticleVM articleVM = new ArticleVM()
            {
                Article = new ServiceApp.Models.Article(),
                ListCategories = _workUnit.Category.GetListCategories()
            };

            if(id != null)
            {
                articleVM.Article = _workUnit.Article.Get(id.GetValueOrDefault());
            }

            return View(articleVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ArticleVM articleVM)
        {
            if (ModelState.IsValid)
            {
                string mainPath = _hostingEnvironment.WebRootPath;
                var files = HttpContext.Request.Form.Files;

                var articleDB = _workUnit.Article.Get(articleVM.Article.ArticleID);

                if (files.Count() > 0)
                {
                    //NEW image for article
                    string Filename = Guid.NewGuid().ToString();
                    var upload = Path.Combine(mainPath, @"img\articles");
                    var extension = Path.GetExtension(files[0].FileName);
                    var newExtension = Path.GetExtension(files[0].FileName);

                    var imagePath = Path.Combine(mainPath, articleDB.UrlImage.TrimStart('\\'));
                    if (System.IO.File.Exists(imagePath))
                    {
                        System.IO.File.Delete(imagePath);
                    }

                    //Article upload
                    using (var fileStreams = new FileStream(Path.Combine(upload, Filename + extension), FileMode.Create))
                    {
                        files[0].CopyTo(fileStreams);
                    }

                    articleVM.Article.UrlImage = @"\img\articles\" + Filename + extension;
                    articleVM.Article.DataCreated = DateTime.Now.ToString();

                    _workUnit.Article.Update(articleVM.Article);
                    _workUnit.Save();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    articleVM.Article.UrlImage = articleDB.UrlImage;
                }

                _workUnit.Article.Update(articleVM.Article);
                _workUnit.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(articleVM);
        }

        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _workUnit.Article.GetAll(includeProperties: "Category" ) });
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var articleDB = _workUnit.Article.Get(id);
            string mainPath = _hostingEnvironment.WebRootPath;
            var imagePath = Path.Combine(mainPath, articleDB.UrlImage.TrimStart('\\'));

            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }
            if (articleDB == null)
            {
                return Json(new { success = false, message = "Error to delete" });
            }

            _workUnit.Article.Remove(articleDB);
            _workUnit.Save();
            return Json(new { success = true, message = "Article deleted !" });

        }
        #endregion
 
    }
}