using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class EnrolmentRepository : BaseReadWriteRepository<Enrolment>, IEnrolmentRepository
    {
        public EnrolmentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(CurriculumBand))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[Enrolment].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[CurriculumBand]", "[CurriculumBand].[Id]", "[Enrolment].[BandId]")}";
        }

        protected override async Task<IEnumerable<Enrolment>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<Enrolment, Student, CurriculumBand, Enrolment>(sql,
                (enrolment, student, band) =>
                {
                    enrolment.Student = student;
                    enrolment.Band = band;

                    return enrolment;
                }, param);
        }
    }
}