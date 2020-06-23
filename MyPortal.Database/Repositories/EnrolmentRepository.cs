using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class EnrolmentRepository : BaseReadWriteRepository<Enrolment>, IEnrolmentRepository
    {
        public EnrolmentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
           
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(CurriculumBand));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.Student", "Student.Id", "Enrolment.StudentId");
            query.LeftJoin("dbo.CurriculumBand", "CurriculumBand.Id", "Enrolment.BandId");
        }

        protected override async Task<IEnumerable<Enrolment>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<Enrolment, Student, CurriculumBand, Enrolment>(sql.Sql,
                (enrolment, student, band) =>
                {
                    enrolment.Student = student;
                    enrolment.Band = band;

                    return enrolment;
                }, sql.NamedBindings);
        }
    }
}