using Arib.EmployeeTaskManagement.Services.DTOs.Employee;
using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arib.EmployeeTaskManagement.Services.Interfaces
{
    public interface IEmployeeService
    {
        public Task<ResponseDTO> GetAllEmployees();
        public Task<ResponseDTO> Add(EmployeeAddEditDTO dto);
        public Task<ResponseDTO> Edit(EmployeeAddEditDTO dto);
        public Task<ResponseDTO> Delete(int id);
        public Task<EmployeeAddEditDTO> GetDto();
    }
}
