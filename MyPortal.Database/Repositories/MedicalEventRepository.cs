using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class MedicalEventRepository : BaseReadWriteRepository<MedicalEvent>, IMedicalEventRepository
    {
        public MedicalEventRepository(ApplicationDbContext context) : base(context, "MedicalEvent")
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(Student), "Student");
            query.SelectAllColumns(typeof(User), "User");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("Students as Student", "Student.Id", "MedicalEvent.StudentId");
            query.LeftJoin("AspNetUsers as User", "User.Id", "MedicalEvent.RecordedById");
        }

        protected override async Task<IEnumerable<MedicalEvent>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<MedicalEvent, Student, User, MedicalEvent>(sql.Sql,
                (medEvent, student, recorder) =>
                {
                    medEvent.Student = student;
                    medEvent.RecordedBy = recorder;

                    return medEvent;
                }, sql.NamedBindings);
        }
    }
}