using Arib.EmployeeTaskManagement.Infrastructure.Interfaces;
using Arib.EmployeeTaskManagement.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Arib.EmployeeTaskManagement.Services.Services
{
    [Authorize]
    public class DropDownService : IDropDownService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DropDownService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        }

        public async Task<List<TResult>> GetDropDownModelAsync<T, TResult>(
            Expression<Func<T, TResult>> selector,
            Expression<Func<T, bool>>? filter = null) where T : class
        {
            var query = _unitOfWork.Repository<T>().GetAllQueryable();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.Select(selector).ToListAsync();
        }
    }
}
