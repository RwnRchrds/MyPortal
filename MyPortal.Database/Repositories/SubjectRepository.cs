using System.Collections.Generic;
using System.Data.Common;
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
    public class SubjectRepository : BaseReadWriteRepository<Subject>, ISubjectRepository
    {
        public SubjectRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            JoinEntity(query, "SubjectCodes", "SC", "SubjectCodeId");

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

            var subjects = await Transaction.Connection.QueryAsync<Subject, SubjectCode, Subject>(sql.Sql,
                (subject, code) =>
                {
                    subject.SubjectCode = code;

                    return subject;
                }, sql.NamedBindings, Transaction);

            return subjects;
        }

        public async Task Update(Subject entity)
        {
            var subject = await Context.Subjects.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (subject == null)
            {
                throw new EntityNotFoundException("Subject not found.");
            }

            subject.Name = entity.Name;
            subject.SubjectCodeId = entity.SubjectCodeId;
        }
    }
}