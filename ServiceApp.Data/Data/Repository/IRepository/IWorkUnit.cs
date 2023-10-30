using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceApp.Data.Data.Repository.IRepository
{
    public interface IWorkUnit : IDisposable
    {
        IJobCategoryRepository JobCategory { get; }
        //Add new repositories 
        IContractorRepository Contractor { get; }
        IEmployeeRepository Employee { get; }

        IArticleRepository Article { get; }

        ICategoryRepository Category { get; }

        ISuburbRepository Suburb { get; }
        ICityRepository City { get; }
        ICountryRepository Country { get; }


        IUserRepository User { get; }

        void Save();
    }
}
