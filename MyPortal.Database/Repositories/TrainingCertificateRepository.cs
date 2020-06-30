using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models;
using SqlKata;

namespace MyPortal.Database.Repositories
{
    public class TrainingCertificateRepository : BaseReadWriteRepository<TrainingCertificate>, ITrainingCertificateRepository
    {
        public TrainingCertificateRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(StaffMember));
            query.SelectAll(typeof(TrainingCourse));
            query.SelectAll(typeof(TrainingCertificateStatus));

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("dbo.StaffMember", "StaffMember.Id", "TrainingCertificate.StaffId");
            query.LeftJoin("dbo.TrainingCourse", "TrainingCourse.Id", "TrainingCertificate.CourseId");
            query.LeftJoin("dbo.TrainingCertificateStatus", "TrainingCertificateStatus.Id",
                "TrainingCertificate.StatusId");
        }

        protected override async Task<IEnumerable<TrainingCertificate>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync<TrainingCertificate, StaffMember, TrainingCourse, TrainingCertificateStatus,
                    TrainingCertificate>(sql.Sql,
                    (certificate, staff, course, status) =>
                    {
                        certificate.StaffMember = staff;
                        certificate.TrainingCourse = course;
                        certificate.Status = status;

                        return certificate;
                    }, sql.NamedBindings);
        }
    }
}