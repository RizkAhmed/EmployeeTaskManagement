using Arib.EmployeeTaskManagement.Services.DTOs.Account;
using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;
using System.Security.Claims;

namespace Arib.EmployeeTaskManagement.Services.Interfaces
{
    public interface IAccountService
    {
        public Task<ClaimsPrincipal> GetUserClaimsPrincipal(AccountDTO accountDTO);
        public Task<ResponseDTO> Logout();
    }
}
