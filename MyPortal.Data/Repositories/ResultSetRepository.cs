using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using MyPortal.Data.Interfaces;
using MyPortal.Data.Models;

namespace MyPortal.Data.Repositories
{
    public class ResultSetRepository : ReadWriteRepository<ResultSet>, IResultSetRepository
    {
        public ResultSetRepository(MyPortalDbContext context) : base(context)
        {
            
        }

        public async Task<IEnumerable<ResultSet>> GetResultSetsByStudent(int studentId)
        {
            return await Context.ResultSets.Where(x => x.Results.Any(r => r.StudentId == studentId))
                .ToListAsync();
        }
    }
}