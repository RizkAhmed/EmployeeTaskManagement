using Arib.EmployeeTaskManagement.Services.DTOs.Department;
using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arib.EmployeeTaskManagement.Services.Interfaces
{
    public interface IDepartmentService
    {
        public Task<ResponseDTO> GetAllDepartments();
        public Task<ResponseDTO> Add(DepartmentAddEditDTO dto);
        public Task<ResponseDTO> Edit(DepartmentAddEditDTO dto);
        public Task<ResponseDTO> Delete(int id);
    }
}
