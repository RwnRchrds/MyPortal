using System;
using System.Threading.Tasks;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IBaseStudentGroupRepository<TEntity> : IReadWriteRepository<TEntity> where TEntity : class, IEntity
    {
        Task<TEntity> GetByStudent(Guid studentId);
    }
}