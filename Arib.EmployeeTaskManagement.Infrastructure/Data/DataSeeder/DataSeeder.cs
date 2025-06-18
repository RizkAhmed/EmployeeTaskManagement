using Arib.EmployeeTaskManagement.Infrastructure.Enums;
using Microsoft.EntityFrameworkCore;

namespace Arib.EmployeeTaskManagement.Infrastructure.Data.DataSeeder
{
    public static class DataSeeder
    {
        public static async Task SeedAsync(ApplicationDbContext context)
        {
            await DepartmentSeeding(context);
            await EmployeeSeeding(context);
            await UserSeeding(context);
            await TaskStatusSeeding(context);
        }

        private static async Task DepartmentSeeding(ApplicationDbContext context)
        {
            if (!context.Departments.Any())
            {
                await context.Database.OpenConnectionAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Departments ON");

                context.Departments.AddRange(InitialData.Departments);
                await context.SaveChangesAsync();

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Departments OFF");
                await context.Database.CloseConnectionAsync();
            }
        }
        private static async Task TaskStatusSeeding(ApplicationDbContext context)
        {
            if (!context.Departments.Any())
            {
                await context.Database.OpenConnectionAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.TaskStatus ON");

                context.TaskStatus.AddRange(InitialData.TaskStatus);
                await context.SaveChangesAsync();

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.TaskStatus OFF");
                await context.Database.CloseConnectionAsync();
            }
        }
        private static async Task EmployeeSeeding(ApplicationDbContext context)
        {
            if (!context.Employees.Any())
            {
                await context.Database.OpenConnectionAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Employees ON");

                context.Employees.AddRange(InitialData.Employees);
                await context.SaveChangesAsync();

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Employees OFF");
                await context.Database.CloseConnectionAsync();
            }
        }

        private static async Task UserSeeding(ApplicationDbContext context)
        {
            if (!context.Users.Any())
            {
                await context.Database.OpenConnectionAsync();
                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Users ON");

                context.Users.AddRange(InitialData.Users);
                await context.SaveChangesAsync();

                await context.Database.ExecuteSqlRawAsync("SET IDENTITY_INSERT dbo.Users OFF");
                await context.Database.CloseConnectionAsync();
            }

            var adminUser = context.Users.FirstOrDefault(u => u.UserName == EUserRole.Admin.ToString());
            if (adminUser is not null)
            {
                foreach (var emp in context.Employees)
                {
                    emp.CreateBy = adminUser.Id;
                }
                await context.SaveChangesAsync();
            }
        }

       
    }
}
