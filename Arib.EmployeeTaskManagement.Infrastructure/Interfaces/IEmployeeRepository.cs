using Arib.EmployeeTaskManagement.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arib.EmployeeTaskManagement.Infrastructure.Interfaces
{
    public interface IEmployeeRepository:IRepository<Employee>
    {
    }
}
