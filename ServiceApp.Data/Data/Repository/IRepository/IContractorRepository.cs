using ServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServiceApp.Data.Data.Repository.IRepository
{
    public interface IContractorRepository : IRepository<Contractor>
    {
        IEnumerable<SelectListItem> GetListContractors();

        void Update(Contractor contractor);
    }
}
