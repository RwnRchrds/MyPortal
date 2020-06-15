using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using MyPortal.Database.Models.Identity;

namespace MyPortal.Database.Repositories
{
    public class MedicalEventRepository : BaseReadWriteRepository<MedicalEvent>, IMedicalEventRepository
    {
        public MedicalEventRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(Student))},
{EntityHelper.GetUserProperties("User")}";

            (query => JoinRelated(query)) = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[MedicalEvent].[StudentId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[AspNetUsers]", "[User].[Id]", "[MedicalEvent].[RecordedById]", "User")}";
        }

        protected override async Task<IEnumerable<MedicalEvent>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<MedicalEvent, Student, ApplicationUser, MedicalEvent>(sql,
                (medEvent, student, recorder) =>
                {
                    medEvent.Student = student;
                    medEvent.RecordedBy = recorder;

                    return medEvent;
                }, param);
        }
    }
}