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
    internal class ContractorRepository : Repository<Contractor>, IContractorRepository
    {
        private readonly ApplicationDbContext _context;

        public ContractorRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<SelectListItem> GetListContractors()
        {
            return _context.Contractors.Select(i => new SelectListItem()
            {
                Text = i.Name,
                Value = i.ContractorID.ToString()
            }
            );
        }

        public Contractor GetContractor(string userId)
        {
            return _context.Contractors.FirstOrDefault(i => i.Id == userId);
        }
        public void Update(Contractor contractor)
        {
            var objCont = _context.Contractors.FirstOrDefault(s => s.ContractorID == contractor.ContractorID);
            objCont.Name = contractor.Name;
            objCont.Phone = contractor.Phone;
            objCont.Email = contractor.Email;
            //objCont.City = contractor.City;
            objCont.Suburb = contractor.Suburb;
            objCont.JobType = contractor.JobType;
            objCont.CategoryID = contractor.CategoryID;
            objCont.SSEEmployer = contractor.SSEEmployer;

            _context.SaveChanges();
        }
    }
}
