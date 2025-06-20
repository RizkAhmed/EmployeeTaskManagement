using Arib.EmployeeTaskManagement.Infrastructure.Interfaces;
using Arib.EmployeeTaskManagement.Infrastructure.Models;
using Arib.EmployeeTaskManagement.Services.DTOs.Account;
using Arib.EmployeeTaskManagement.Services.DTOs.GenericDTOs;
using Arib.EmployeeTaskManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace Arib.EmployeeTaskManagement.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ClaimsPrincipal> GetUserClaimsPrincipal(AccountDTO accountDTO)
        {

            var account = await _unitOfWork.Repository<User>()
                .GetFirstAsync(u => u.UserName == accountDTO.userName && u.Password == accountDTO.Password);
            if (account is null)
                return null;

            ClaimsIdentity claims = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            claims.AddClaim(new Claim(ClaimTypes.Name, account.UserName));
            claims.AddClaim(new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()));
            claims.AddClaim(new Claim(ClaimTypes.Role, account.Role));
            claims.AddClaim(new Claim("EmployeeId", account.EmployeeId.ToString()));

            ClaimsPrincipal principal = new ClaimsPrincipal(claims);
            return principal;

        }

        public Task<ResponseDTO> Logout()
        {
            throw new NotImplementedException();
        }
    }
}
