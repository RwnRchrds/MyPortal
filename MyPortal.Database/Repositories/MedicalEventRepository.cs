using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class MedicalEventRepository : BaseReadWriteRepository<MedicalEvent>, IMedicalEventRepository
    {
        public MedicalEventRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(ApplicationUser), "User");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Student", "Student.Id", "MedicalEvent.StudentId");
            query.LeftJoin("dbo.AspNetUsers as User", "User.Id", "MedicalEvent.RecordedById");
        }

        protected override async Task<IEnumerable<MedicalEvent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<MedicalEvent, Student, ApplicationUser, MedicalEvent>(sql.Sql,
                (medEvent, student, recorder) =>
                {
                    medEvent.Student = student;
                    medEvent.RecordedBy = recorder;

                    return medEvent;
                }, sql.NamedBindings);
        }
    }
}