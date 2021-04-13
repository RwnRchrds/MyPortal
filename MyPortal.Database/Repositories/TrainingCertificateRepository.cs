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
    public class TrainingCertificateRepository : BaseReadWriteRepository<TrainingCertificate>, ITrainingCertificateRepository
    {
        public TrainingCertificateRepository(ApplicationDbContext context, DbTransaction transaction) : base(context, transaction, "TrainingCertificate")
        {
            
        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StaffMember), "StaffMember");
            query.SelectAllColumns(typeof(TrainingCourse), "TrainingCourse");
            query.SelectAllColumns(typeof(TrainingCertificateStatus), "TrainingCertificateStatus");

            JoinRelated(query);
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("StaffMembers as StaffMember", "StaffMember.Id", "TrainingCertificate.StaffId");
            query.LeftJoin("TrainingCourses as TrainingCourse", "TrainingCourse.Id", "TrainingCertificate.CourseId");
            query.LeftJoin("TrainingCertificateStatus", "TrainingCertificateStatus.Id",
                "TrainingCertificate.StatusId");
        }

        protected override async Task<IEnumerable<TrainingCertificate>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Transaction.Connection
                .QueryAsync<TrainingCertificate, StaffMember, TrainingCourse, TrainingCertificateStatus,
                    TrainingCertificate>(sql.Sql,
                    (certificate, staff, course, status) =>
                    {
                        certificate.StaffMember = staff;
                        certificate.TrainingCourse = course;
                        certificate.Status = status;

                        return certificate;
                    }, sql.NamedBindings, Transaction);
        }

        public async Task Update(TrainingCertificate entity)
        {
            var certificate = await Context.TrainingCertificates.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (certificate == null)
            {
                throw new EntityNotFoundException("Training certificate not found.");
            }

            certificate.StatusId = entity.StatusId;
        }
    }
}