using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class GiftedTalentedRepository : BaseReadWriteRepository<GiftedTalented>, IGiftedTalentedRepository
    {
        public GiftedTalentedRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction)
        {

        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("Students as S", "S.Id", $"{TblAlias}.StudentId");
            query.LeftJoin("Subjects as SU", "SU.Id", $"{TblAlias}.SubjectId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "S");
            query.SelectAllColumns(typeof(Subject), "SU");

            return query;
        }

        protected override async Task<IEnumerable<GiftedTalented>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var giftedTalented =
                await Transaction.Connection.QueryAsync<GiftedTalented, Student, Subject, GiftedTalented>(sql.Sql,
                    (giftedTalented, student, subject) =>
                    {
                        giftedTalented.Student = student;
                        giftedTalented.Subject = subject;

                        return giftedTalented;
                    }, sql.NamedBindings, Transaction);

            return giftedTalented;
        }

        public async Task<IEnumerable<GiftedTalented>> GetByStudent(Guid studentId)
        {
            var query = GenerateQuery();

            query.Where($"{TblAlias}.StudentId", studentId);

            return await ExecuteQuery(query);
        }

        public async Task Update(GiftedTalented entity)
        {
            var giftedTalented = await Context.GiftedTalented.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (giftedTalented == null)
            {
                throw new EntityNotFoundException("Gifted talented subject not found.");
            }

            giftedTalented.Notes = entity.Notes;
            giftedTalented.SubjectId = entity.SubjectId;
        }
    }
}