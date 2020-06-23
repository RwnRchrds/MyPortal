using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AspectRepository : BaseReadWriteRepository<Aspect>, IAspectRepository
    {
        public AspectRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(AspectType));
            query.SelectAll(typeof(GradeSet));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.AspectType", "AspectType.Id", "Aspect.TypeId");
            query.LeftJoin("dbo.GradeSet", "GradeSet.Id", "Aspect.GradeSetId");
        }

        protected override async Task<IEnumerable<Aspect>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Aspect, AspectType, GradeSet, Aspect>(sql.Sql, (aspect, type, gradeSet) =>
            {
                aspect.Type = type;
                aspect.GradeSet = gradeSet;

                return aspect;
            }, sql.NamedBindings);
        }
    }
}