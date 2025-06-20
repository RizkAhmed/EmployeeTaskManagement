using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arib.EmployeeTaskManagement.Services.DTOs.Department
{
    public record DepartmentDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string InsertionDate { get; set; }
        public int CountOfEmployees { get; set; }
        public decimal CountOfEmpsSaleries { get; set; }
    }

}
