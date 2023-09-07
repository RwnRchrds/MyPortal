using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.BaseTypes;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IReadRepository<TEntity> where TEntity : class, IEntity
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<TEntity> GetById(Guid id);
        Task<IEnumerable<AuditLog>> GetAuditLogsById(Guid id);
        Task<IEnumerable<AuditLog>> GetAllAuditLogs();
    }
}
