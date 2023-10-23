using ServiceApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ServiceApp.Data.Data.Repository.IRepository
{
    public interface IEmployeeRepository : IRepository<Employee>
    {
        IEnumerable<SelectListItem> GetListEmployees();

        public Employee GetEmployee(string id);
        void Update(Employee employee);
    }
}
