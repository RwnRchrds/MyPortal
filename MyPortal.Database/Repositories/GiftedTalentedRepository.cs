using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class GiftedTalentedRepository : BaseReadWriteRepository<GiftedTalented>, IGiftedTalentedRepository
    {
        public GiftedTalentedRepository(ApplicationDbContext context) : base(context, "GiftedTalented")
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

            return await Connection.QueryAsync<GiftedTalented, Student, Subject, GiftedTalented>(sql.Sql,
                (gt, student, subject) =>
                {
                    gt.Student = student;
                    gt.Subject = subject;

                    return gt;
                }, sql.NamedBindings);
        }
    }
}