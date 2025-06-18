using Arib.EmployeeTaskManagement.Infrastructure.Data;
using Arib.EmployeeTaskManagement.Infrastructure.Implementation;
using Arib.EmployeeTaskManagement.Infrastructure.Interfaces;
using Arib.EmployeeTaskManagement.Services.Interfaces;
using Arib.EmployeeTaskManagement.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Arib.EmployeeTaskManagement.Infrastructure.Extentions
{
    public static class ServicesRegistration
    {
        public static WebApplicationBuilder AddServicesRegistration(this WebApplicationBuilder builder)
        {

            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<ITaskService, TaskService>();
            return builder;
        }
    }
}
