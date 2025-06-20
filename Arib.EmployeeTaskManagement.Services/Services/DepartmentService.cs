using Arib.EmployeeTaskManagement.Infrastructure.Interfaces;
using Arib.EmployeeTaskManagement.Infrastructure.Models;
using Arib.EmployeeTaskManagement.Services.DTOs.Department;
using Arib.EmployeeTaskManagement.Services.DTOs.Employee;
using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;
using Arib.EmployeeTaskManagement.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace Arib.EmployeeTaskManagement.Services.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> Add(DepartmentAddEditDTO dto)
        {
            try
            {
                var dept = new Department
                {
                    Name = dto.Name,
                    CreateBy = _unitOfWork.ClaimsService.UserId,
                };

                await _unitOfWork.Repository<Department>().AddAsync(dept);

                if (!await _unitOfWork.CommitAsync())
                {
                    return new ResponseDTO(false, "There was an error during saving", null);
                }

                return new ResponseDTO(true, "Saved successfully", dept.Id);
            }
            catch (Exception)
            {

                return new ResponseDTO(false, "An error occurred during saving", null);
            }
        }

        public async Task<ResponseDTO> Delete(int id)
        {
            try
            {
                var dept = await _unitOfWork.Repository<Department>()
                    .GetAllQueryable()
                    .Include(d=>d.Employees)
                    .FirstOrDefaultAsync(d=>d.Id == id);
                if (dept is null)
                    return new ResponseDTO(false, "Department not found.", null);

                if(dept.Employees.Any())
                    return new ResponseDTO(false, "Cantn't delete the department", null);

                dept.IsDeleted = true;
                dept.UpdateDate = DateTime.Now;
                dept.UpdateBy = _unitOfWork.ClaimsService.UserId;
                dept.DeleteDate = DateTime.Now;
                dept.DeleteBy = _unitOfWork.ClaimsService.UserId;
                _unitOfWork.Repository<Department>().Update(dept);
                if (await _unitOfWork.CommitAsync())
                {
                    return new ResponseDTO(true, "Department deleted successfully.", dept.Id);
                }

                return new ResponseDTO(false, "Failed to save deletion changes.", null);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(false, "An unexpected error occurred.", null);
            }
        }

        public async Task<ResponseDTO> Edit(DepartmentAddEditDTO dto)
        {
            try
            {
                var dbDept = await _unitOfWork.Repository<Department>().GetByIdAsync(dto.Id.Value);

                if(dbDept is null)
                    return new ResponseDTO(false, "Department not found.", null);

                dbDept.Name = dto.Name;
                dbDept.UpdateDate = DateTime.Now;
                dbDept.UpdateBy = _unitOfWork.ClaimsService.UserId;

                _unitOfWork.Repository<Department>().Update(dbDept);

                if (!await _unitOfWork.CommitAsync())
                {
                    return new ResponseDTO(false, "There was an error during saving", null);
                }

                return new ResponseDTO(true, "Saved successfully", dbDept.Id);
            }
            catch (Exception)
            {

                return new ResponseDTO(false, "An error occurred during saving", null);
            }
        }

        public async Task<ResponseDTO> GetAllDepartments()
        {
            try
            {
                var employees = await _unitOfWork.Repository<Department>().GetDTOsAsync(
                    selector: e => new DepartmentDTO
                    {
                        Id = e.Id,
                        Name = e.Name,
                        InsertionDate = (e.CreateDate ?? DateTime.MinValue).ToString("yyyy-MM-dd HH:mm"),
                        CountOfEmployees = e.Employees.Count(),
                        CountOfEmpsSaleries = e.Employees.Sum(e => e.Salary)
                    },
                    predicate: e => !e.IsDeleted
                );

                return new ResponseDTO(true, string.Empty, employees);

            }
            catch (Exception ex)
            {
                return new ResponseDTO(false, ex.Message, new List<EmployeeDTO>() { });
            }
        }
    }
}
