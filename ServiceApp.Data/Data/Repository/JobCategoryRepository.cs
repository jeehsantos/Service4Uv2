using ServiceApp.Data.Data.Repository;
using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServiceApp.Data.Data.Repository
{
    internal class JobCategoryRepository : Repository<JobCategory>, IJobCategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public JobCategoryRepository(ApplicationDbContext context) : base(context)  
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetListCategories()
        {
            return _context.JobCategories.Select(i => new SelectListItem() { 
                Text = i.CategoryName,
                Value = i.JobCategoryID.ToString()
            }
            );
        }

        public void Update(JobCategory jobCategory)
        {
            var objCat = _context.JobCategories.FirstOrDefault(s => s.JobCategoryID == jobCategory.JobCategoryID);
            objCat.CategoryName = jobCategory.CategoryName;
            objCat.CategoryType = jobCategory.CategoryType;

            _context.SaveChanges();
        }
    }
}
