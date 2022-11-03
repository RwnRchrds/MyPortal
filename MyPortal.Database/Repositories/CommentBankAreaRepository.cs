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

namespace MyPortal.Database.Repositories;

public class CommentBankAreaRepository : BaseReadWriteRepository<CommentBankArea>, ICommentBankAreaRepository
{
    public CommentBankAreaRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
    {
    }

    protected override Query JoinRelated(Query query)
    {
        query.LeftJoin("CommentBanks as CB", "CB.Id", $"{TblAlias}.CommentBankId");
        query.LeftJoin("Courses as C", "C.Id", $"{TblAlias}.CourseId");

        return query;
    }

    protected override Query SelectAllRelated(Query query)
    {
        query.SelectAllColumns(typeof(CommentBank), "CB");
        query.SelectAllColumns(typeof(Course), "C");

        return query;
    }

    protected override async Task<IEnumerable<CommentBankArea>> ExecuteQuery(Query query)
    {
        var sql = Compiler.Compile(query);

        var areas = await Transaction.Connection.QueryAsync<CommentBankArea, CommentBank, Course, CommentBankArea>(sql.Sql,
            (area, bank, course) =>
            {
                area.CommentBank = bank;
                area.Course = course;

                return area;
            }, sql.NamedBindings, Transaction);

        return areas;
    }
}