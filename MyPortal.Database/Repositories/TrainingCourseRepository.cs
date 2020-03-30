using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class TrainingCourseRepository : BaseReadWriteRepository<TrainingCourse>, ITrainingCourseRepository
    {
        public TrainingCourseRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
        }

        protected override async Task<IEnumerable<TrainingCourse>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<TrainingCourse>(sql, param);
        }
    }
}