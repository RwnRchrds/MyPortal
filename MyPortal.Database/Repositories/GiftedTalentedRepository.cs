using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;
using Task = System.Threading.Tasks.Task;

namespace MyPortal.Database.Repositories
{
    public class GiftedTalentedRepository : BaseReadWriteRepository<GiftedTalented>, IGiftedTalentedRepository
    {
        public GiftedTalentedRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(Subject))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[GiftedTalented].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Subject]", "[Subject].[Id]", "[GiftedTalented].[SubjectId]")}";
        }

        protected override async Task<IEnumerable<GiftedTalented>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<GiftedTalented, Student, Subject, GiftedTalented>(sql,
                (gt, student, subject) =>
                {
                    gt.Student = student;
                    gt.Subject = subject;

                    return gt;
                }, param);
        }

        public async Task Update(GiftedTalented entity)
        {
            var giftedTalentedInDb = await Context.GiftedTalented.FindAsync(entity.Id);

            giftedTalentedInDb.SubjectId = entity.SubjectId;
            giftedTalentedInDb.Notes = entity.Notes;
        }
    }
}