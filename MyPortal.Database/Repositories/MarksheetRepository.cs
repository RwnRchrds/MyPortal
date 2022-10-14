using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class MarksheetRepository : BaseReadWriteRepository<Marksheet>, IMarksheetRepository
    {
        public MarksheetRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "StudentGroups", "SG", "StudentGroupId");
            JoinEntity(query, "MarksheetTemplates", "MT", "MarksheetTemplateId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StudentGroup), "SG");
            query.SelectAllColumns(typeof(MarksheetTemplate), "MT");

            return query;
        }

        protected override async Task<IEnumerable<Marksheet>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var templateGroups =
                await Transaction.Connection
                    .QueryAsync<Marksheet, StudentGroup, MarksheetTemplate, Marksheet>(
                        sql.Sql,
                        (templateGroup, studentGroup, template) =>
                        {
                            templateGroup.StudentGroup = studentGroup;
                            templateGroup.Template = template;

                            return templateGroup;
                        }, sql.NamedBindings, Transaction);

            return templateGroups;
        }
    }
}