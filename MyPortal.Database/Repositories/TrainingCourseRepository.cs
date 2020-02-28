using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

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

        public async Task Update(TrainingCourse entity)
        {
            var course = await Context.TrainingCourses.FindAsync(entity.Id);

            course.Description = entity.Description;
            course.Code = entity.Code;
        }
    }
}