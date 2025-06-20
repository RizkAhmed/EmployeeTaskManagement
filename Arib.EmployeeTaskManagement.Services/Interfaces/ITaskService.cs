using Arib.EmployeeTaskManagement.Services.DTOs.Employee;
using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;
using Arib.EmployeeTaskManagement.Services.DTOs.Task;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arib.EmployeeTaskManagement.Services.Interfaces
{
    public interface ITaskService
    {
        Task<ResponseDTO> GetAllTasksAsync();
        Task<ResponseDTO> AddTaskAsync(TaskAddEditDTO dto);
        Task<ResponseDTO> EditTaskAsync(TaskAddEditDTO dto);
        Task<ResponseDTO> DeleteTaskAsync(int id);
        Task<ResponseDTO> GetEmployeeTasksAsync();
        Task<ResponseDTO> UpdateStatusAsync(TaskChangeStatusDTO dto);

        public Task<TaskAddEditDTO> GetDto();

    }
}
