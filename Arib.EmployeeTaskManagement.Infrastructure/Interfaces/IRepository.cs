using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Arib.EmployeeTaskManagement.Infrastructure.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAllQueryable();
        Task<List<T>> GetAllAsync();

        Task<TResult> GetDTOAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate);
        Task<List<TResult>> GetDTOsAsync<TResult>(Expression<Func<T, TResult>> selector, Expression<Func<T, bool>> predicate);

        Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate);
        Task<List<T>> GetAllWhereAsync(Expression<Func<T, bool>> predicate);
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        Task AddRangeAsync(List<T> entitys);

        void Update(T entity);
        void Delete(T entity);
        Task<bool> SaveChangesAsync();
    }
}
