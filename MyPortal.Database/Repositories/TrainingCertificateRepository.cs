using System.Collections.Generic;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using MyPortal.Database.Exceptions;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces.Repositories;
using MyPortal.Database.Models.Connection;
using MyPortal.Database.Models.Entity;
using MyPortal.Database.Repositories.Base;
using SqlKata;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class TrainingCertificateRepository : BaseReadWriteRepository<TrainingCertificate>,
        ITrainingCertificateRepository
    {
        public TrainingCertificateRepository(DbUserWithContext dbUser) : base(dbUser)
        {
        }

        protected override Query JoinRelated(Query query)
        {
            query.LeftJoin("StaffMembers as S", "S.Id", $"{TblAlias}.StaffId");
            query.LeftJoin("TrainingCourses as TC", "TC.Id", $"{TblAlias}.CourseId");
            query.LeftJoin("TrainingCertificateStatus as TCS", "TCS.Id", $"{TblAlias}.StatusId");

            return query;
        }

        protected override Query SelectAllRelated(Query query)
        {
            query.SelectAllColumns(typeof(StaffMember), "SM");
            query.SelectAllColumns(typeof(TrainingCourse), "TC");
            query.SelectAllColumns(typeof(TrainingCertificateStatus), "TCS");

            return query;
        }

        protected override async Task<IEnumerable<TrainingCertificate>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            var certificates =
                await DbUser.Transaction.Connection
                    .QueryAsync<TrainingCertificate, StaffMember, TrainingCourse, TrainingCertificateStatus,
                        TrainingCertificate>(sql.Sql,
                        (certificate, staff, course, status) =>
                        {
                            certificate.StaffMember = staff;
                            certificate.TrainingCourse = course;
                            certificate.Status = status;

                            return certificate;
                        }, sql.NamedBindings, DbUser.Transaction);

            return certificates;
        }

        public async Task Update(TrainingCertificate entity)
        {
            var certificate = await DbUser.Context.TrainingCertificates.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (certificate == null)
            {
                throw new EntityNotFoundException("Training certificate not found.");
            }

            certificate.StatusId = entity.StatusId;
        }
    }
}