using Arib.EmployeeTaskManagement.Infrastructure.Data;
using Arib.EmployeeTaskManagement.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;

namespace Arib.EmployeeTaskManagement.Infrastructure.Implementation
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly Dictionary<Type, object> _repositories = new();

        private readonly ApplicationDbContext _context;

        public IFileService FileService { get; }
        public IClaimsService ClaimsService { get; }

        public IRepository<T> Repository<T>() where T : class
        {
            if (!_repositories.ContainsKey(typeof(T)))
            {
                _repositories[typeof(T)] = new Repository<T>(_context);
            }

            return (IRepository<T>)_repositories[typeof(T)];
        }
        public UnitOfWork(
            ApplicationDbContext context,
            IFileService fileService,
            IClaimsService claimsService)
        {
            _context = context;
            FileService = fileService;
            ClaimsService = claimsService;
        }



        public async Task<bool> CommitAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
