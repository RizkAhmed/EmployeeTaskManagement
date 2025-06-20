namespace Arib.EmployeeTaskManagement.Services.DTOs.Task
{
    public record TaskChangeStatusDTO
    {
        public int Id { get; set; }
        public int StatusId { get; set; }
    }
}
