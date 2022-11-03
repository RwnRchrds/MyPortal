﻿using System.Collections.Generic;
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

public class CommentBankSectionRepository : BaseReadWriteRepository<CommentBankSection>, ICommentBankSectionRepository
{
    public CommentBankSectionRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
    {
    }

    protected override Query JoinRelated(Query query)
    {
        query.LeftJoin("CommentBankAreas as CBA", "CBA.Id", $"{TblAlias}.CommentBankAreaId");

        return query;
    }

    protected override Query SelectAllRelated(Query query)
    {
        query.SelectAllColumns(typeof(CommentBankArea), "CBA");

        return query;
    }

    protected override async Task<IEnumerable<CommentBankSection>> ExecuteQuery(Query query)
    {
        var sql = Compiler.Compile(query);

        var sections = await Transaction.Connection.QueryAsync<CommentBankSection, CommentBankArea, CommentBankSection>(
            sql.Sql,
            (section, area) =>
            {
                section.Area = area;

                return section;
            }, sql.NamedBindings, Transaction);

        return sections;
    }
}