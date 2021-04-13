using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class AttendanceCodeRepository : BaseReadWriteRepository<AttendanceCode>, IAttendanceCodeRepository
    {
        public AttendanceCodeRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "AttendanceCode")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(AttendanceCodeMeaning), "AttendanceCodeMeaning");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("AttendanceCodeMeanings as AttendanceCodeMeaning", "AttendanceCodeMeaning.Id", "AttendanceCode.MeaningId");
        }

        protected override async Task<IEnumerable<AttendanceCode>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<AttendanceCode, AttendanceCodeMeaning, AttendanceCode>(sql.Sql,
                (code, meaning) =>
                {
                    code.CodeMeaning = meaning;

                    return code;
                }, sql.NamedBindings, Transaction);
        }

        public async Task<AttendanceCode> GetByCode(string code)
        {
            var query = GenerateQuery();

            query.Where("AttendanceCode.Code", "=", code);

            return (await ExecuteQuery(query)).FirstOrDefault();
        }

        public async Task<IEnumerable<AttendanceCode>> GetAll(bool activeOnly, bool includeRestricted)
        {
            var query = GenerateQuery(false, false);

            if (activeOnly)
            {
                query.Where("AttendanceCode.Active", true);
            }

            if (!includeRestricted)
            {
                query.Where("AttendanceCode.Restricted", false);
            }

            return await ExecuteQuery<AttendanceCode>(query);
        }

        public async Task Update(AttendanceCode entity)
        {
            var code = await Context.AttendanceCodes.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (code == null)
            {
                throw new EntityNotFoundException("Attendance code not found.");
            }

            if (code.System)
            {
                throw new SystemEntityException("System entities cannot be modified.");
            }

            code.Code = entity.Code;
            code.Description = entity.Description;
            code.Active = entity.Active;
            code.MeaningId = entity.MeaningId;
        }
    }
}