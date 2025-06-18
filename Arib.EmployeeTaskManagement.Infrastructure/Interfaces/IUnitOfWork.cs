using Arib.EmployeeTaskManagement.Infrastructure.Interfaces;

namespace Arib.EmployeeTaskManagement.Infrastructure.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IFileService FileService { get; }

        IRepository<T> Repository<T>() where T : class;
        Task<bool> CommitAsync();

    }
}
