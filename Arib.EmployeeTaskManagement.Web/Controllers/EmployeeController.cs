using Arib.EmployeeTaskManagement.Infrastructure.Enums;
using Arib.EmployeeTaskManagement.Services.DTOs.Employee;
using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;
using Arib.EmployeeTaskManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arib.EmployeeTaskManagement.Web.Controllers
{
    [Authorize(Roles = $"{nameof(EUserRole.Admin)}")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _employeeService.GetDto());
        }

        public async Task<IActionResult> GetAllEmployee()
        {
            return Json(await _employeeService.GetAllEmployees());
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEditEmployee([FromForm] EmployeeAddEditDTO dto)
        {
            if (ModelState.IsValid)
            {
                if (dto.Id > 0)
                    return Json(await _employeeService.Edit(dto));
                return Json(await _employeeService.Add(dto));
            }
            return Json(new ResponseDTO(false, "Please check inserted data", null));
            
        }
        public async Task<IActionResult> Delete(int id)
        {
            return Json(await _employeeService.Delete(id));
        }
    }
}
