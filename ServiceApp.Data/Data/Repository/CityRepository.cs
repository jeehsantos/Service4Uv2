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
    internal class CityRepository : Repository<City>, ICityRepository
    {
        private readonly ApplicationDbContext _context;

        public CityRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetListCities()
        {
            return _context.Cities.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.CityID.ToString()
            }
            );
        }
 
        
    }
}
