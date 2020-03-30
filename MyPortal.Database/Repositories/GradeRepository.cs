using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class GradeRepository : BaseReadWriteRepository<Grade>, IGradeRepository
    {
        public GradeRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
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
    }
}