using Arib.EmployeeTaskManagement.Infrastructure.Data.DataSeeder;
using Arib.EmployeeTaskManagement.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;


namespace Arib.EmployeeTaskManagement.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeTask> Tasks { get; set; }
        public DbSet<Task_Status> TaskStatus { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        }
    }

}
