using System.ComponentModel.DataAnnotations.Schema;

namespace Arib.EmployeeTaskManagement.Infrastructure.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public int? CreateBy { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? UpdateBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsDeleted { get; set; }
        public int? DeleteBy { get; set; }
        public DateTime? DeleteDate { get; set; }


        [ForeignKey(nameof(CreateBy))]
        public User? CreatedUser { get; set; }
        [ForeignKey(nameof(UpdateBy))]
        public User? UpdatedUser { get; set; }
        [ForeignKey(nameof(DeleteBy))]
        public User? DeletedUser { get; set; }
    }

}
