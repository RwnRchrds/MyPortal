using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Models.QueryResults.Assessment;

namespace MyPortal.Database.Interfaces.Repositories
{
    public interface IResultRepository : IReadWriteRepository<Result>, IUpdateRepository<Result>
    {
        Task<Result> GetResult(Guid studentId, Guid aspectId, Guid resultSetId);
        Task<IEnumerable<Result>> GetPreviousResults(Guid studentId, Guid aspectId, DateTime dateTo);
        Task<IEnumerable<ResultDetailModel>> GetResultDetailsByMarksheet(Guid marksheetId);
    }
}