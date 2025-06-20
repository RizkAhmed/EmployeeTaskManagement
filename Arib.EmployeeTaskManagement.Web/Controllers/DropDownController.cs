using Arib.EmployeeTaskManagement.Infrastructure.Models;
using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;
using Arib.EmployeeTaskManagement.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Arib.EmployeeTaskManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DropDownController : ControllerBase
    {
        private readonly IDropDownService _dropDownService;

        public DropDownController(IDropDownService dropDownService)
        {
            _dropDownService = dropDownService;
        }

        [HttpGet("GetEmployees")]
        public async Task<IActionResult> GetEmployees(int? id)
        {
            var result = await _dropDownService.GetDropDownModelAsync<Employee, LookUp>(e => new LookUp
            {
                Value = e.Id,
                Text = $"{e.FirstName} {e.LastName}"
            }, e => !e.IsDeleted && (id == null || (e.Id != id)));
            return Ok(result);
        }
    }
}
