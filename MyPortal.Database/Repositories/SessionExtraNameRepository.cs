using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using Org.BouncyCastle.Crypto.Tls;
using SqlKata;

namespace MyPortal.Database.Repositories;

public class SessionExtraNameRepository : BaseReadWriteRepository<SessionExtraName>, ISessionExtraNameRepository
{
    public SessionExtraNameRepository(DbUserWithContext dbUser) : base(dbUser)
    {
    }

    protected override Query JoinRelated(Query query)
    {
        query.LeftJoin("AttendanceWeeks as AW", "AW.Id", "SEN.AttendanceWeekId");
        query.LeftJoin("Sessions as S", "S.Id", "SEN.SessionId");
        query.LeftJoin("Students as ST", "ST.Id", "SEN.StudentId");
        
        return query;
    }

    protected override Query SelectAllRelated(Query query)
    {
        query.SelectAllColumns(typeof(AttendanceWeek), "AW");
        query.SelectAllColumns(typeof(Session), "S");
        query.SelectAllColumns(typeof(Student), "ST");

        return query;
    }

    protected override async Task<IEnumerable<SessionExtraName>> ExecuteQuery(Query query)
    {
        var sql = Compiler.Compile(query);

        var extraNames = await DbUser.Transaction.Connection
            .QueryAsync<SessionExtraName, AttendanceWeek, Session, Student, SessionExtraName>(sql.Sql,
                (extraName, week, session, student) =>
                {
                    extraName.AttendanceWeek = week;
                    extraName.Session = session;
                    extraName.Student = student;

                    return extraName;
                }, sql.NamedBindings, DbUser.Transaction);

        return extraNames;
    }

    public async Task<IEnumerable<SessionExtraName>> GetExtraNamesBySession(Guid sessionId, Guid attendanceWeekId)
    {
        var query = GetDefaultQuery();

        query.Where("SEN.SessionId", sessionId);
        query.Where("SEN.AttendanceWeekId", attendanceWeekId);

        return await ExecuteQuery(query);
    }
}