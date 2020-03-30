using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class TrainingCertificateRepository : BaseReadWriteRepository<TrainingCertificate>, ITrainingCertificateRepository
    {
        public TrainingCertificateRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(StaffMember))},
{EntityHelper.GetAllColumns(typeof(TrainingCourse))},
{EntityHelper.GetAllColumns(typeof(TrainingCertificateStatus))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[StaffMember]", "[StaffMember].[Id]", "[TrainingCertificate].[StaffId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[TrainingCourse]", "[TrainingCourse].[Id]", "[TrainingCertificate].[CourseId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[TrainingCertificateStatus]", "[TrainingCertificateStatus].[Id]", "[TrainingCertificate].[StatusId]")}";
        }

        protected override async Task<IEnumerable<TrainingCertificate>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection
                .QueryAsync<TrainingCertificate, StaffMember, TrainingCourse, TrainingCertificateStatus,
                    TrainingCertificate>(sql,
                    (certificate, staff, course, status) =>
                    {
                        certificate.StaffMember = staff;
                        certificate.TrainingCourse = course;
                        certificate.Status = status;

                        return certificate;
                    }, param);
        }
    }
}