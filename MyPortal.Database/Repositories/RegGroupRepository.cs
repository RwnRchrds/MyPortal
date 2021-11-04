using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Constants;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class RegGroupRepository : BaseStudentGroupRepository<RegGroup>, IRegGroupRepository
    {
        public RegGroupRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
            
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "StudentGroups", "SG", "StudentGroupId");
            JoinEntity(query, "YearGroups", "YG", "YearGroupId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StudentGroup), "SG");
            query.SelectAllColumns(typeof(YearGroup), "YG");

            return query;
        }

        protected override async Task<IEnumerable<RegGroup>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var regGroups = await Transaction.Connection.QueryAsync<RegGroup, StudentGroup, YearGroup, RegGroup>(
                sql.Sql,
                (reg, studentGroup, year) =>
                {
                    reg.StudentGroup = studentGroup;
                    reg.YearGroup = year;

                    return reg;
                }, sql.NamedBindings, Transaction);

            return regGroups;
        }
    }
}