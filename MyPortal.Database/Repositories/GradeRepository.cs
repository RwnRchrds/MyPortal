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
    public class GradeRepository : BaseReadWriteRepository<Grade>, IGradeRepository
    {
        public GradeRepository(IDbConnection connection, string tblAlias = null) : base(connection, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(GradeSet))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[GradeSet]", "[GradeSet].[Id]", "[Grade].[GradeSetId]")}";
        }

        protected override async Task<IEnumerable<Grade>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Grade, GradeSet, Grade>(sql, (grade, set) =>
            {
                grade.GradeSet = set;

                return grade;
            }, param);
        }

        public async Task Update(Grade entity)
        {
            var gradeInDb = await Context.Grades.FindAsync(entity.Id);

            gradeInDb.Code = entity.Code;
            gradeInDb.Value = entity.Value;
            gradeInDb.GradeSetId = entity.GradeSetId;
        }
    }
}