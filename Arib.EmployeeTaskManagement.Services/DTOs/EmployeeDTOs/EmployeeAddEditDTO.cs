using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;
using Microsoft.AspNetCore.Http;


namespace Arib.EmployeeTaskManagement.Services.DTOs.EmployeeDTOs
{
    public class EmployeeAddEditDTO
    {
        public int? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public decimal Salary { get; set; }
        public IFormFile ImageFile { get; set; }
        public int? ManagerId { get; set; }
        public int DepartmentId { get; set; }
        public IEnumerable<LookUp>? Manangers { get; set; }
        public IEnumerable<LookUp>? Departments { get; set; }
    }
}
