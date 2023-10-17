using ServiceApp.Data.Data.Repository;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceApp.Data.Data.Repository
{
    internal class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetListCategories()
        {
            return _context.Categories.Select(i => new SelectListItem()
            {
                Text = i.CategoryName,
                Value = i.CategoryID.ToString()
            }
            );
        }

        public void Update(Category category)
        {
            var objCat = _context.Categories.FirstOrDefault(s => s.CategoryID == category.CategoryID);
            objCat.CategoryName = category.CategoryName;
            objCat.Sort = category.Sort;

            _context.SaveChanges();
        }
    }
}
