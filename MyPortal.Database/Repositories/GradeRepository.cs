using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class GradeRepository : BaseReadWriteRepository<Grade>, IGradeRepository
    {
        public GradeRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(GradeSet));

            query = JoinRelated(query);

            return query;
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("dbo.GradeSet", "GradeSet.Id", "Grade.GradeSetId");

            return query;
        }

        protected override async Task<IEnumerable<Grade>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Grade, GradeSet, Grade>(sql.Sql, (grade, set) =>
            {
                grade.GradeSet = set;

                return grade;
            }, sql.Bindings);
        }
    }
}