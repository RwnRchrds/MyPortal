using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class AspectRepository : BaseReadWriteRepository<Aspect>, IAspectRepository
    {
        public AspectRepository(ApplicationDbContext context) : base(context, "Aspect")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AspectType), "AspectType");
            query.SelectAllColumns(typeof(GradeSet), "GradeSet");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("AspectTypes as AspectType", "AspectType.Id", "Aspect.TypeId");
            query.LeftJoin("GradeSets as GradeSet", "GradeSet.Id", "Aspect.GradeSetId");
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