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
    public class PersonAttachmentRepository : BaseReadWriteRepository<PersonAttachment>, IPersonAttachmentRepository
    {
        public PersonAttachmentRepository(IDbConnection connection, ApplicationDbContext context) : base(connection, context)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(Person))},
{EntityHelper.GetAllColumns(typeof(Document))}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[Person].[Id]", "[PersonAttachment].[PersonId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Document]", "[Document].[Id]", "[PersonAttachment].[DocumentId]")}";
        }

        protected override async Task<IEnumerable<PersonAttachment>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection.QueryAsync<PersonAttachment, Person, Document, PersonAttachment>(sql,
                (attachment, person, document) =>
                {
                    attachment.Person = person;
                    attachment.Document = document;

                    return attachment;
                }, param);
        }

        public async Task Update(PersonAttachment entity)
        {
            var attachment = await Context.PersonAttachments.FindAsync(entity.Id);

            attachment.DocumentId = entity.DocumentId;
            attachment.PersonId = entity.PersonId;
        }
    }
}