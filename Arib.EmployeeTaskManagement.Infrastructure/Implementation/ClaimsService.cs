using Arib.EmployeeTaskManagement.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Arib.EmployeeTaskManagement.Services.Services
{
    public class ClaimsService : IClaimsService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public int UserId
        {
            get
            {
                var userIdClaim = GetClaimValue(ClaimTypes.NameIdentifier);
                int.TryParse(userIdClaim, out int userId);
                return userId;
            }
        }



        public string UserName => GetClaimValue(ClaimTypes.Name);


        public string UserRole => GetClaimValue(ClaimTypes.Role);

        public int EmployeeId
        {
            get
            {
                var EmpIdClaim = GetClaimValue("EmployeeId");
                int.TryParse(EmpIdClaim, out int empId);
                return empId;
            }
        }

        private string GetClaimValue(string claimName)
        {
            var user = _httpContextAccessor.HttpContext.User;
            return user.Identity.IsAuthenticated ? user.FindFirst(claimName)?.Value ?? string.Empty : string.Empty;
        }
    }
}
