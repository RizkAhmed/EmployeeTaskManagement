using Arib.EmployeeTaskManagement.Infrastructure.Models;

namespace Arib.EmployeeTaskManagement.Infrastructure.Interfaces
{
    public interface IClaimsService
    {
        string UserName { get; }
        int UserId { get; }
        string UserRole { get; }
        int EmployeeId { get; }
    }
}
