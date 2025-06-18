using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arib.EmployeeTaskManagement.Infrastructure.Interfaces
{
    public interface IFileService
    {
        Task<string> SaveFileAsync(IFormFile file);
        void DeleteFile(string relativePath);
    }
}
