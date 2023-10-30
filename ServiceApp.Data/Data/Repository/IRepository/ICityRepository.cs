using ServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ServiceApp.Data.Data.Repository.IRepository
{
    public interface ICityRepository : IRepository<City>
    {
        IEnumerable<SelectListItem> GetListCities();
 
    }
}
