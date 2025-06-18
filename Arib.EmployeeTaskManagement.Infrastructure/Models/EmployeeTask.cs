
using Arib.EmployeeTaskManagement.Infrastructure.Enums;
using System.ComponentModel.DataAnnotations;


namespace Arib.EmployeeTaskManagement.Infrastructure.Models
{
    public class EmployeeTask:BaseEntity
    {

        [Required]
        public string Title { get; set; } = string.Empty;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int StatusId { get; set; } = (int)Enums.ETaskStatus.Pending;

        public Task_Status? Status { get; set; }

        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
    }

}
