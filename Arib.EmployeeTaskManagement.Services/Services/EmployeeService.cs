using Arib.EmployeeTaskManagement.Infrastructure.Interfaces;
using Arib.EmployeeTaskManagement.Infrastructure.Models;
using Arib.EmployeeTaskManagement.Services.DTOs.EmployeeDTOs;
using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;
using Arib.EmployeeTaskManagement.Services.Interfaces;
namespace Arib.EmployeeTaskManagement.Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ResponseDTO> Add(EmployeeAddEditDTO dto)
        {
            if (dto.ImageFile == null || dto.ImageFile.Length == 0)
                return new ResponseDTO(false, "Please upload a valid file", null);

            string imagePath = string.Empty;

            try
            {
                imagePath = await _unitOfWork.FileService.SaveFileAsync(dto.ImageFile);

                var emp = new Employee
                {
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Salary = dto.Salary,
                    ImagePath = imagePath,
                    DepartmentId = dto.DepartmentId,
                    ManagerId = dto.ManagerId,
                    CreateBy = 1, // TODO: Replace with logged-in user ID
                    CreateDate = DateTime.Now
                };

                await _unitOfWork.Repository<Employee>().AddAsync(emp);

                bool saved = await _unitOfWork.CommitAsync();
                if (!saved)
                {
                    _unitOfWork.FileService.DeleteFile(imagePath); // rollback image
                    return new ResponseDTO(false, "There was an error during saving", null);
                }

                return new ResponseDTO(true, "Saved successfully", emp.Id); 
            }
            catch (Exception)
            {
                if (!string.IsNullOrEmpty(imagePath))
                    _unitOfWork.FileService.DeleteFile(imagePath);

                return new ResponseDTO(false, "An error occurred during saving", null);
            }
        }


        public async Task<ResponseDTO> Edit(EmployeeAddEditDTO dto)
        {
            if (dto.Id == null)
                return new ResponseDTO(false, "Employee ID is required.", null);

            string imagePath = string.Empty;

            try
            {
                var dbEmp = await _unitOfWork.Repository<Employee>().GetByIdAsync(dto.Id.Value);
                if (dbEmp == null)
                    return new ResponseDTO(false, "Employee not found.", null);

                // Update fields
                dbEmp.FirstName = dto.FirstName;
                dbEmp.LastName = dto.LastName;
                dbEmp.DepartmentId = dto.DepartmentId;
                dbEmp.ManagerId = dto.ManagerId;
                dbEmp.Salary = dto.Salary;
                dbEmp.UpdateDate = DateTime.Now;
                dbEmp.UpdateBy = 1; // TODO: Replace with logged-in user ID

                // Handle file update
                if (dto.ImageFile is { Length: > 0 })
                {
                    // Delete the old file
                    if (!string.IsNullOrEmpty(dbEmp.ImagePath))
                    {
                        _unitOfWork.FileService.DeleteFile(dbEmp.ImagePath);
                    }

                    // Save new file
                    dbEmp.ImagePath = imagePath = await _unitOfWork.FileService.SaveFileAsync(dto.ImageFile);
                }

                _unitOfWork.Repository<Employee>().Update(dbEmp);

                if (!await _unitOfWork.CommitAsync())
                {
                    if (!string.IsNullOrEmpty(imagePath))
                        _unitOfWork.FileService.DeleteFile(imagePath); // rollback

                    return new ResponseDTO(false, "Failed to save changes.", null);
                }

                return new ResponseDTO(true, "Employee updated successfully.", dbEmp.Id);
            }
            catch (Exception ex)
            {
                if (!string.IsNullOrEmpty(imagePath))
                {
                    _unitOfWork.FileService.DeleteFile(imagePath);
                }

                return new ResponseDTO(false, "An unexpected error occurred.", null);
            }
        }

        public async Task<ResponseDTO> Delete(int id)
        {
            try
            {
                var emp = await _unitOfWork.Repository<Employee>().GetByIdAsync(id);
                if (emp is null)
                {
                    return new ResponseDTO(false, "Employee not found.", null);
                }

                emp.IsDeleted = true;
                emp.UpdateDate = DateTime.Now;
                emp.UpdateBy = 1;
                emp.DeleteDate = DateTime.Now;
                emp.DeleteBy = 1;
                _unitOfWork.Repository<Employee>().Update(emp);
                if (await _unitOfWork.CommitAsync())
                {
                    return new ResponseDTO(true, "Employee deleted successfully.", emp.Id);
                }

                return new ResponseDTO(false, "Failed to save deletion changes.", null);
            }
            catch (Exception ex)
            {
                return new ResponseDTO(false, "An unexpected error occurred.", null);
            }
        }
        public async Task<ResponseDTO> GetAllEmployees()
        {
            try
            {
                var employees = await _unitOfWork.Repository<Employee>().GetDTOsAsync(
                    selector: e => new EmployeeDTO
                    {
                        Id = e.Id,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        InsertionDate = (e.CreateDate ?? DateTime.MinValue).ToString("yyyy-MM-dd HH:mm"),
                        Salary = e.Salary,
                        ImagePath = e.ImagePath,
                        DepartmentId = e.DepartmentId,
                        DepartmentName = e.Department != null ? e.Department.Name : string.Empty,
                        ManagerId = e.ManagerId,
                        ManagerName = e.Manager != null
                            ? string.Concat(e.Manager.FirstName, " ", e.Manager.LastName)
                            : null
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

        public async Task<EmployeeAddEditDTO> GetDto()
        {
            var dto = new EmployeeAddEditDTO();

            dto.Manangers = await _unitOfWork.Repository<Employee>().GetDTOsAsync(e => new LookUp
            {
                Value = e.Id,
                Text = $"{e.FirstName} {e.LastName}"
            },
            e => !e.IsDeleted);

            dto.Departments = await _unitOfWork.Repository<Department>().GetDTOsAsync(d => new LookUp
            {
                Value = d.Id,
                Text = d.Name
            },
            d => !d.IsDeleted);
            return dto;
        }
    }
}
