using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;

namespace Arib.EmployeeTaskManagement.Services.DTOs.Task
{
    public record TaskAddEditDTO
    {
        public int? Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public int EmployeeId { get; set; }
        public IEnumerable<LookUp>? Employees { get; set; }

    }
}
