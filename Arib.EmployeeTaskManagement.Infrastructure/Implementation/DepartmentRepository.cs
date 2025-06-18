using Arib.EmployeeTaskManagement.Infrastructure.Data;
using Arib.EmployeeTaskManagement.Infrastructure.Interfaces;
using Arib.EmployeeTaskManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace Arib.EmployeeTaskManagement.Infrastructure.Implementation
{
    public class DepartmentRepository : Repository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
