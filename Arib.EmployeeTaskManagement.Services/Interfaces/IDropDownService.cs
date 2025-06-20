using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Arib.EmployeeTaskManagement.Services.Interfaces
{
    public interface IDropDownService
    {
        Task<List<TResult>> GetDropDownModelAsync<T, TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>>? filter) where T : class;
    }
}
