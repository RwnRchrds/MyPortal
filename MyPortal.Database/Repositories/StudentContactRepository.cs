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
    public class StudentContactRepository : BaseReadWriteRepository<StudentContact>, IStudentContactRepository
    {
        public StudentContactRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetAllColumns(typeof(RelationshipType))},
{EntityHelper.GetAllColumns(typeof(Student))},
{EntityHelper.GetAllColumns(typeof(Person), "StudentPerson")}
{EntityHelper.GetAllColumns(typeof(Contact))},
{EntityHelper.GetAllColumns(typeof(Person), "ContactPerson")}";

            JoinRelated = $@"
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[RelationshipType]", "[RelationshipType].[Id]", "[StudentContact].[RelationshipTypeId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[StudentContact].[StudentId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[StudentPerson].[Id]", "[Student].[PersonId]", "StudentPerson")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Contact]", "[Contact].[Id]", "[StudentContact].[ContactId]")}
{SqlHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[ContactPerson].[Id]", "[Contact].[PersonId]", "ContactPerson")}";
        }

        protected override async Task<IEnumerable<StudentContact>> ExecuteQuery(string sql, object param = null)
        {
            return await Connection
                .QueryAsync<StudentContact, RelationshipType, Student, Person, Contact, Person, StudentContact>(sql,
                    (sContact, relationship, student, sPerson, contact, cPerson) =>
                    {
                        sContact.RelationshipType = relationship;
                        sContact.Student = student;
                        sContact.Student.Person = sPerson;
                        sContact.Contact = contact;
                        sContact.Contact.Person = cPerson;

                        return sContact;
                    }, param);
        }

        public async Task Update(StudentContact entity)
        {
            var studentContact = await Context.StudentContacts.FindAsync(entity.Id);

            studentContact.ContactId = entity.ContactId;
            studentContact.RelationshipTypeId = entity.RelationshipTypeId;
            studentContact.ParentalResponsibility = entity.ParentalResponsibility;
            studentContact.CourtOrder = entity.CourtOrder;
            studentContact.PupilReport = entity.PupilReport;
            studentContact.Correspondence = entity.Correspondence;
        }
    }
}