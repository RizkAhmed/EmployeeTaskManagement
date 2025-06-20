using Arib.EmployeeTaskManagement.Infrastructure.Enums;
using Arib.EmployeeTaskManagement.Services.DTOs.Department;
using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;
using Arib.EmployeeTaskManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Arib.EmployeeTaskManagement.Web.Controllers
{
    [Authorize(Roles = $"{nameof(EUserRole.Admin)}")]
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> GetAllEmployee()
        {
            return Json(await _departmentService.GetAllDepartments());
        }
        [HttpPost]
        public async Task<IActionResult> AddOrEditDepartment(DepartmentAddEditDTO dto)
        {
            if (ModelState.IsValid)
            {
                if (dto.Id > 0)
                    return Json(await _departmentService.Edit(dto));
                return Json(await _departmentService.Add(dto));
            }
            return Json(new ResponseDTO(false, "Please check inserted data", null));

        }
        public async Task<IActionResult> Delete(int id)
        {
            return Json(await _departmentService.Delete(id));
        }
    }
}
