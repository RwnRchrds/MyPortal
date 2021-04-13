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
        public GiftedTalentedRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "GiftedTalented")
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(Subject), "Subject");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Students as Student", "Student.Id", "GiftedTalented.StudentId");
            query.LeftJoin("Subjects as Subject", "Subject.Id", "GiftedTalented.SubjectId");
        }

        protected override async Task<IEnumerable<GiftedTalented>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection.QueryAsync<GiftedTalented, Student, Subject, GiftedTalented>(sql.Sql,
                (gt, student, subject) =>
                {
                    gt.Student = student;
                    gt.Subject = subject;

                    return gt;
                }, sql.NamedBindings, Transaction);
        }

        public async Task<IEnumerable<GiftedTalented>> GetByStudent(Guid studentId)
        {
            var query = GenerateQuery();

            query.Where("GiftedTalented.StudentId", studentId);

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