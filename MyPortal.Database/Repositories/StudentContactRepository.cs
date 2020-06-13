using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using MyPortal.Database.Helpers;
using MyPortal.Database.Interfaces;
using MyPortal.Database.Models;

namespace MyPortal.Database.Repositories
{
    public class StudentContactRepository : BaseReadWriteRepository<StudentContact>, IStudentContactRepository
    {
        public StudentContactRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {
            RelatedColumns = $@"
{EntityHelper.GetPropertyNames(typeof(RelationshipType))},
{EntityHelper.GetPropertyNames(typeof(Student))},
{EntityHelper.GetPropertyNames(typeof(Person), "StudentPerson")}
{EntityHelper.GetPropertyNames(typeof(Contact))},
{EntityHelper.GetPropertyNames(typeof(Person), "ContactPerson")}";

            JoinRelated = $@"
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[RelationshipType]", "[RelationshipType].[Id]", "[StudentContact].[RelationshipTypeId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Student]", "[Student].[Id]", "[StudentContact].[StudentId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[StudentPerson].[Id]", "[Student].[PersonId]", "StudentPerson")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Contact]", "[Contact].[Id]", "[StudentContact].[ContactId]")}
{QueryHelper.Join(JoinType.LeftJoin, "[dbo].[Person]", "[ContactPerson].[Id]", "[Contact].[PersonId]", "ContactPerson")}";
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
    }
}