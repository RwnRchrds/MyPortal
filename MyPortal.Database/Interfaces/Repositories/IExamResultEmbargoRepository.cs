using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IExamResultEmbargoRepository : IReadWriteRepository<ExamResultEmbargo>, IUpdateRepository<ExamResultEmbargo>
    {
        Task<ExamResultEmbargo> GetByResultSetId(Guid resultSetId);
    }
}