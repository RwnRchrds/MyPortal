using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class ResultRepository : ReadWriteRepository<Result>, IResultRepository
    {
        public ResultRepository(MyPortalDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<Result>> GetResultsByStudent(int studentId, int resultSetId)
        {
            return await Context.Results.Where(x => x.StudentId == studentId && x.ResultSetId == resultSetId).ToListAsync();
        }
    }
}