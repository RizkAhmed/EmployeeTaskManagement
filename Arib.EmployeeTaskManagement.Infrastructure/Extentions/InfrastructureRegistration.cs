﻿
using Arib.EmployeeTaskManagement.Infrastructure.Data;
using Arib.EmployeeTaskManagement.Infrastructure.Helpers;
using Arib.EmployeeTaskManagement.Infrastructure.Implementation;
using Arib.EmployeeTaskManagement.Infrastructure.Interfaces;
using Arib.EmployeeTaskManagement.Services.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Arib.EmployeeTaskManagement.Infrastructure.Extentions
{
    public static class InfrastructureRegistration
    {
        public static WebApplicationBuilder AddInfrastructureRegistration(this WebApplicationBuilder builder)
        {
            // DbContext
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.Configure<FileSettings>(builder.Configuration.GetSection("FileSettings"));
            builder.Services.AddScoped<IFileService, FileService>(); 
            builder.Services.AddScoped<IClaimsService, ClaimsService>();

            return builder;
        }
    }
}
