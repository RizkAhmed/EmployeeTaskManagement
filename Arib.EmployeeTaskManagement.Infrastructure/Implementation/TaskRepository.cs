using Arib.EmployeeTaskManagement.Infrastructure.Data;
using Arib.EmployeeTaskManagement.Infrastructure.Interfaces;
using Arib.EmployeeTaskManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Arib.EmployeeTaskManagement.Infrastructure.Implementation
{
    public class TaskRepository : Repository<EmployeeTask>, ITaskRepository
    {
        public TaskRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
