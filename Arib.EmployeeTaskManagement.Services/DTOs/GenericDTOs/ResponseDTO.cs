using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs
{
    public record ResponseDTO(bool IsSuccess, string Message, object? Data);
    
}
