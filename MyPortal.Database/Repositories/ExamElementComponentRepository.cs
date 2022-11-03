using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class ExamElementComponentRepository : BaseReadWriteRepository<ExamElementComponent>, IExamElementComponentRepository
    {
        public ExamElementComponentRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ExamElements as EE", "EE.Id", $"{TblAlias}.ElementId");
            query.LeftJoin("ExamComponent as EC", "EC.Id", $"{TblAlias}.ComponentId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamElement), "EE");
            query.SelectAllColumns(typeof(ExamComponent), "EC");

            return query;
        }

        protected override async Task<IEnumerable<ExamElementComponent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var elementComponents =
                await Transaction.Connection
                    .QueryAsync<ExamElementComponent, ExamElement, ExamComponent, ExamElementComponent>(sql.Sql,
                        (eec, element, component) =>
                        {
                            eec.Element = element;
                            eec.Component = component;

                            return eec;
                        }, sql.NamedBindings, Transaction);

            return elementComponents;
        }
    }
}