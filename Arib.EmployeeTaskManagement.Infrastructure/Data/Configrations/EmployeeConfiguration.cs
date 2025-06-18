using Arib.EmployeeTaskManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arib.EmployeeTaskManagement.Infrastructure.Data.Configrations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasOne(e => e.Manager)
                   .WithMany(e => e.Subordinates)
                   .HasForeignKey(e => e.ManagerId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.CreatedUser)
                   .WithMany()
                   .HasForeignKey(e => e.CreateBy)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.UpdatedUser)
                   .WithMany()
                   .HasForeignKey(e => e.UpdateBy)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(e => e.DeletedUser)
                   .WithMany()
                   .HasForeignKey(e => e.DeleteBy)
                   .OnDelete(DeleteBehavior.Restrict);
            builder.Property(e => e.Salary)
                .HasPrecision(18, 2);
        }
    }

}
