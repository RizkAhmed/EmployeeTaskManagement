using Arib.EmployeeTaskManagement.Infrastructure.Enums;

namespace Arib.EmployeeTaskManagement.Infrastructure.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }
           

}
