using ServiceApp.Data.Data.Repository.IRepository;
using ServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Data.Data.Repository
{
    public class WorkUnit : IWorkUnit
    {
        private readonly ApplicationDbContext _context;

        public WorkUnit(ApplicationDbContext context)
        {
            _context = context;
            JobCategory = new JobCategoryRepository(_context);
            Contractor = new ContractorRepository(_context);
            Employee = new EmployeeRepository(_context);
            Article = new ArticleRepository(_context);
            Category = new CategoryRepository(_context);
            User = new UserRepository(_context);
        }

        public IJobCategoryRepository JobCategory { get; private set; }
        public IContractorRepository Contractor { get; private set; }
        public IEmployeeRepository Employee { get; private set; }

        public IArticleRepository Article { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public IUserRepository User { get; private set; }

        public void Dispose()
        {
           _context.Dispose();
        }
        public void Save()
        {
            _context.SaveChanges();
        }

       
    }
}
