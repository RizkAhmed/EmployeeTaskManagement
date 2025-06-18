using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arib.EmployeeTaskManagement.Infrastructure.Models
{
    public class Employee : BaseEntity
    {

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public decimal Salary { get; set; }

        public string? ImagePath { get; set; }

        public int? ManagerId { get; set; }
        public Employee? Manager { get; set; }
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }

        public ICollection<Employee>? Subordinates { get; set; }
        public ICollection<EmployeeTask>? Tasks { get; set; }
    }
}