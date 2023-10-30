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
    internal class CountryRepository : Repository<Country>, ICountryRepository
    {
        private readonly ApplicationDbContext _context;

        public CountryRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetListCountries()
        {
            return _context.Countries.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.CountryID.ToString()
            }
            );
        }
 
        
    }
}
