using System.Collections.Generic;
using System.Data;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class CurriculumBandMembershipRepository : BaseReadWriteRepository<CurriculumBandMembership>, ICurriculumBandMembershipRepository
    {
        public CurriculumBandMembershipRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
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
            query.LeftJoin("Student", "Student.Id", "CurriculumBandMembership.StudentId");
            query.LeftJoin("CurriculumBand", "CurriculumBand.Id", "CurriculumBandMembership.BandId");
        }

        protected override async Task<IEnumerable<CurriculumBandMembership>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection.QueryAsync<CurriculumBandMembership, Student, CurriculumBand, CurriculumBandMembership>(sql.Sql,
                (enrolment, student, band) =>
                {
                    enrolment.Student = student;
                    enrolment.Band = band;

                    return enrolment;
                }, sql.NamedBindings);
        }
    }
}