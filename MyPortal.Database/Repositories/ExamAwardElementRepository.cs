using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class ExamAwardElementRepository : BaseReadWriteRepository<ExamAwardElement>, IExamAwardElementRepository
    {
        public ExamAwardElementRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("ExamAwards as EA", "EA.Id", $"{TblAlias}.AwardId");
            query.LeftJoin("ExamElements as EE", "EE.Id", $"{TblAlias}.ElementId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(ExamAward), "EA");
            query.SelectAllColumns(typeof(ExamElement), "EE");

            return query;
        }

        protected override async Task<IEnumerable<ExamAwardElement>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var examAwardElements =
                await DbUser.Transaction.Connection
                    .QueryAsync<ExamAwardElement, ExamAward, ExamElement, ExamAwardElement>(
                        sql.Sql,
                        (eae, award, element) =>
                        {
                            eae.Award = award;
                            eae.Element = element;

                            return eae;
                        }, sql.NamedBindings, DbUser.Transaction);

            return examAwardElements;
        }
    }
}