﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AttendanceCodeRepository : BaseReadWriteRepository<AttendanceCode>, IAttendanceCodeRepository
    {
        public AttendanceCodeRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("AttendanceCodeMeanings as ACM", "ACM.Id", $"{TblAlias}.MeaningId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AttendanceCodeType), "ACM");

            return query;
        }

        protected override async Task<IEnumerable<AttendanceCode>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var codes = await DbUser.Transaction.Connection
                .QueryAsync<AttendanceCode, AttendanceCodeType, AttendanceCode>(sql.Sql,
                    (code, meaning) =>
                    {
                        code.CodeType = meaning;

                        return code;
                    }, sql.NamedBindings, DbUser.Transaction);

            return codes;
        }

        public async Task<AttendanceCode> GetByCode(string code)
        {
            var query = GetDefaultQuery();

            query.Where($"{TblAlias}.Code", "=", code);

            return (await ExecuteQuery(query)).FirstOrDefault();
        }

        public async Task<IEnumerable<AttendanceCode>> GetAll(bool activeOnly, bool includeRestricted)
        {
            var query = GetDefaultQuery();

            if (activeOnly)
            {
                query.Where($"{TblAlias}.Active", true);
            }

            if (!includeRestricted)
            {
                query.Where($"{TblAlias}.Restricted", false);
            }

            return await ExecuteQuery<AttendanceCode>(query);
        }

        public async Task Update(AttendanceCode entity)
        {
            var code = await DbUser.Context.AttendanceCodes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (code == null)
            {
                throw new EntityNotFoundException("Attendance code not found.");
            }

            code.Code = entity.Code;
            code.Description = entity.Description;
            code.Active = entity.Active;
            code.AttendanceCodeTypeId = entity.AttendanceCodeTypeId;
        }
    }
}