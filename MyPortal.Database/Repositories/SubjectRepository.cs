using System.Collections.Generic;
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
    public class SubjectRepository : BaseReadWriteRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("SubjectCodes as SC", "SC.Id", $"{TblAlias}.SubjectCodeId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(SubjectCode), "SC");

            return query;
        }

        protected override async Task<IEnumerable<Subject>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var subjects = await DbUser.Transaction.Connection.QueryAsync<Subject, SubjectCode, Subject>(sql.Sql,
                (subject, code) =>
                {
                    subject.SubjectCode = code;

                    return subject;
                }, sql.NamedBindings, DbUser.Transaction);

            return subjects;
        }

        public async Task Update(Subject entity)
        {
            var subject = await DbUser.Context.Subjects.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (subject == null)
            {
                throw new EntityNotFoundException("Subject not found.");
            }

            subject.Name = entity.Name;
            subject.SubjectCodeId = entity.SubjectCodeId;
        }
    }
}