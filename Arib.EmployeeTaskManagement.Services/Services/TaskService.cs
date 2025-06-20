using Arib.EmployeeTaskManagement.Infrastructure.Enums;
using Arib.EmployeeTaskManagement.Infrastructure.Interfaces;
using Arib.EmployeeTaskManagement.Infrastructure.Models;
using Arib.EmployeeTaskManagement.Services.DTOs.Employee;
using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;
using Arib.EmployeeTaskManagement.Services.DTOs.Task;
using Arib.EmployeeTaskManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Arib.EmployeeTaskManagement.Services.Services
{
    public class TaskService : ITaskService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TaskService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> AddTaskAsync(TaskAddEditDTO dto)
        {
            try
            {
                var task = new EmployeeTask
                {
                    Title = dto.Title,
                    Description = dto.Description,
                    EmployeeId = dto.EmployeeId,
                    CreateBy = _unitOfWork.ClaimsService.UserId
                };

                await _unitOfWork.Repository<EmployeeTask>().AddAsync(task);

                if (!await _unitOfWork.CommitAsync())
                {
                    return new ResponseDTO(false, "Failed to save task.", null);
                }

                return new ResponseDTO(true, "Task saved successfully.", task.Id);
            }
            catch
            {
                return new ResponseDTO(false, "An error occurred while saving the task.", null);
            }
        }

        public async Task<ResponseDTO> EditTaskAsync(TaskAddEditDTO dto)
        {
            try
            {
                var task = await _unitOfWork.Repository<EmployeeTask>().GetByIdAsync(dto.Id.Value);
                if (task == null)
                    return new ResponseDTO(false, "Task not found.", null);

                task.Title = dto.Title;
                task.Description = dto.Description;
                task.EmployeeId = dto.EmployeeId;
                task.UpdateBy = _unitOfWork.ClaimsService.UserId;
                task.UpdateDate = DateTime.Now;

                _unitOfWork.Repository<EmployeeTask>().Update(task);

                if (!await _unitOfWork.CommitAsync())
                {
                    return new ResponseDTO(false, "Failed to save task changes.", null);
                }

                return new ResponseDTO(true, "Task updated successfully.", task.Id);
            }
            catch
            {
                return new ResponseDTO(false, "An error occurred while updating the task.", null);
            }
        }

        public async Task<ResponseDTO> DeleteTaskAsync(int id)
        {
            try
            {
                var task = await _unitOfWork.Repository<EmployeeTask>().GetByIdAsync(id);
                if (task == null)
                    return new ResponseDTO(false, "Task not found.", null);

                if(task.StatusId != (int)ETaskStatus.Pending)
                    return new ResponseDTO(false, "You can only delete pending tasks", null);

                task.IsDeleted = true;
                task.DeleteBy = _unitOfWork.ClaimsService.UserId;
                task.DeleteDate = DateTime.Now;
                task.UpdateDate = DateTime.Now;

                _unitOfWork.Repository<EmployeeTask>().Update(task);

                if (!await _unitOfWork.CommitAsync())
                    return new ResponseDTO(false, "Failed to delete task.", null);

                return new ResponseDTO(true, "Task deleted successfully.", task.Id);
            }
            catch
            {
                return new ResponseDTO(false, "An error occurred while deleting the task.", null);
            }
        }

        public async Task<ResponseDTO> GetAllTasksAsync()
        {
            try
            {
                var tasks = await _unitOfWork.Repository<EmployeeTask>().GetDTOsAsync(
                    t => new TaskDTO
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        StatusId = t.StatusId,
                        StatusName = $"{t.Status.Icon} {t.Status.Name} ",
                        EmployeeId = t.EmployeeId,
                        EmployeeName = $"{t.Employee.FirstName} {t.Employee.LastName}",
                        InsertionDate = (t.CreateDate ?? DateTime.MinValue).ToString("yyyy-MM-dd HH:mm")
                    },
                    t => !t.IsDeleted && t.Employee.ManagerId == _unitOfWork.ClaimsService.UserId
                );

                return new ResponseDTO(true, string.Empty, tasks);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(false, ex.Message, new List<TaskDTO>() { });
            }
        }

        public async Task<TaskAddEditDTO> GetDto()
        {
            var dto = new TaskAddEditDTO();

            dto.Employees = await _unitOfWork.Repository<Employee>().GetDTOsAsync(e => new LookUp
            {
                Value = e.Id,
                Text = $"{e.FirstName} {e.LastName}"
            },
            e => !e.IsDeleted && e.ManagerId == _unitOfWork.ClaimsService.UserId);

            return dto;
        }

        public async Task<ResponseDTO> GetEmployeeTasksAsync()
        {
            try
            {
                var tasks = await _unitOfWork.Repository<EmployeeTask>().GetDTOsAsync(
                    t => new TaskDTO
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        StatusId = t.StatusId,
                        StatusName = $"{t.Status.Icon} {t.Status.Name} ",
                        EmployeeId = t.CreatedUser.EmployeeId,
                        EmployeeName = $"{t.CreatedUser.Employee.FirstName} {t.CreatedUser.Employee.LastName}",
                        InsertionDate = (t.CreateDate ?? DateTime.MinValue).ToString("yyyy-MM-dd HH:mm")
                    },
                     t => !t.IsDeleted && t.EmployeeId ==  _unitOfWork.ClaimsService.EmployeeId
                );

                return new ResponseDTO(true, string.Empty, tasks);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(false, ex.Message, new List<TaskDTO>() { });
            }
        }

        public async Task<ResponseDTO> UpdateStatusAsync(TaskChangeStatusDTO dto)
        {
            try
            {
                var task = await _unitOfWork.Repository<EmployeeTask>().GetByIdAsync(dto.Id);
                if (task == null)
                    return new ResponseDTO(false, "Task not found.", null);

                task.StatusId = dto.StatusId;
                task.UpdateBy = _unitOfWork.ClaimsService.UserId;
                task.UpdateDate = DateTime.Now;

                _unitOfWork.Repository<EmployeeTask>().Update(task);
                if (await _unitOfWork.CommitAsync())
                    return new ResponseDTO(true, "Status updated successfully.", task.Id);

                return new ResponseDTO(false, "Failed to update status.", null);
            }
            catch
            {
                return new ResponseDTO(false, "An error occurred while updating status.", null);
            }
        }
    }
}
