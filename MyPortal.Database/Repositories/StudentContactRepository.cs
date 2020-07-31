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
    public class StudentContactRepository : BaseReadWriteRepository<StudentContactRelationship>, IStudentContactRepository
    {
        public StudentContactRepository(IDbConnection connection, ApplicationDbContext context, string tblAlias = null) : base(connection, context, tblAlias)
        {

        }

        protected override void SelectAllRelated(Query query)
        {
            query.SelectAll(typeof(ContactRelationshipType));
            query.SelectAll(typeof(Student));
            query.SelectAll(typeof(Person), "StudentPerson");
            query.SelectAll(typeof(Contact));
            query.SelectAll(typeof(Person), "ContactPerson");
        }

        protected override void JoinRelated(Query query)
        {
            query.LeftJoin("RelationshipType", "RelationshipType.Id", "StudentContact.RelationshipTypeId");
            query.LeftJoin("Student", "Student.Id", "StudentContact.StudentId");
            query.LeftJoin("Person as StudentPerson", "StudentPerson.Id", "Student.PersonId");
            query.LeftJoin("Contact", "Contact.Id", "StudentContact.ContactId");
            query.LeftJoin("Person as ContactPerson", "ContactPerson.Id", "Contact.PersonId");
        }

        protected override async Task<IEnumerable<StudentContactRelationship>> ExecuteQuery(Query query)
        {
            var sql = Compiler.Compile(query);

            return await Connection
                .QueryAsync<StudentContactRelationship, ContactRelationshipType, Student, Person, Contact, Person, StudentContactRelationship>(sql.Sql,
                    (sContact, relationship, student, sPerson, contact, cPerson) =>
                    {
                        sContact.RelationshipType = relationship;
                        sContact.Student = student;
                        sContact.Student.Person = sPerson;
                        sContact.Contact = contact;
                        sContact.Contact.Person = cPerson;

                        return sContact;
                    }, sql.NamedBindings);
        }
    }
}