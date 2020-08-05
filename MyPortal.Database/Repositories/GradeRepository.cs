using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class GradeRepository : BaseReadWriteRepository<Grade>, IGradeRepository
    {
        public GradeRepository(ApplicationDbContext context) : base(context, "Grade")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(GradeSet));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("GradeSets as GradeSet", "GradeSet.Id", "Grade.GradeSetId");
        }

        protected override async Task<IEnumerable<Grade>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Grade, GradeSet, Grade>(sql.Sql, (grade, set) =>
            {
                grade.GradeSet = set;

                return grade;
            }, sql.NamedBindings);
        }
    }
}