using Arib.EmployeeTaskManagement.Services.DTOs.Task;
using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;
using Arib.EmployeeTaskManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Arib.EmployeeTaskManagement.Services.Services;
using Arib.EmployeeTaskManagement.Infrastructure.Enums;

namespace Arib.EmployeeTaskManagement.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [Authorize(Roles = $"{nameof(EUserRole.Manager)}")]
        public async Task<IActionResult> Index()
        {
            return View(await _taskService.GetDto());
        }

        [HttpGet]
        [Authorize(Roles = $"{nameof(EUserRole.Manager)}")]
        public async Task<IActionResult> GetAllTasks()
        {
            var result = await _taskService.GetAllTasksAsync();
            return Json(result);
        }

        [HttpPost]
        [Authorize(Roles = $"{nameof(EUserRole.Manager)}")]
        public async Task<IActionResult> AddOrEditTask(TaskAddEditDTO dto)
        {
            if (ModelState.IsValid)
            {
                if (dto.Id > 0)
                    return Json(await _taskService.EditTaskAsync(dto));

                return Json(await _taskService.AddTaskAsync(dto));
            }

            return Json(new ResponseDTO(false, "Please check inserted data", null));
        }

        [HttpPost]
        [Authorize(Roles = $"{nameof(EUserRole.Manager)}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _taskService.DeleteTaskAsync(id);
            return Json(result);
        }

        [Authorize(Roles = $"{nameof(EUserRole.Regular)}")]
        public async Task<IActionResult> MyTasks()
        {
            return View(await _taskService.GetEmployeeTasksAsync());
        }
        [Authorize(Roles = $"{nameof(EUserRole.Regular)}")]
        public async Task<IActionResult> GetEmployeeTasks()
        {
            return Json(await _taskService.GetEmployeeTasksAsync());
        }
        [Authorize(Roles = $"{nameof(EUserRole.Regular)}")]
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(TaskChangeStatusDTO dto)
        {
            var result = await _taskService.UpdateStatusAsync(dto);
            return Json(result);
        }
    } 
}
