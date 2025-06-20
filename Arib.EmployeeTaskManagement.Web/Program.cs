using Arib.EmployeeTaskManagement.Infrastructure.Data.DataSeeder;
using Arib.EmployeeTaskManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Arib.EmployeeTaskManagement.Infrastructure.Extentions;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Arib.EmployeeTaskManagement.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.AddInfrastructureRegistration();
            builder.AddServicesRegistration();
            builder.Services.AddHttpContextAccessor();

            var app = builder.Build();
            using (var scope = app.Services.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                await DataSeeder.SeedAsync(dbContext);
            }
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Account}/{action=Login}/{id?}")
                .WithStaticAssets();

            app.Run();
        }
    }
}
