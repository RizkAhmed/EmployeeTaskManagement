using Arib.EmployeeTaskManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Arib.EmployeeTaskManagement.Infrastructure.Data.Configrations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasOne(u => u.Employee)
                   .WithOne()
                   .HasForeignKey<User>(u => u.EmployeeId);
        }
    }

}
